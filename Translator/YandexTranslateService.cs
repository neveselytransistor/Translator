using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Translator
{
    public class YandexTranslateService : ITranslateService
    {
        private readonly string _key;

        public YandexTranslateService(IConfiguration configuration)
        {
            _key = configuration.GetSection("yandex:key").Value;
        }
        public async Task<string> TranslateAsync(string input, string language)
        {
            var host = "https://translate.yandex.net";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(host);

                var requestData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("key", _key),
                    new KeyValuePair<string, string>("text", input),
                    new KeyValuePair<string, string>("lang", language)
                };
                var requestContent = new FormUrlEncodedContent(requestData);

                var response = await client.PostAsync("/api/v1.5/tr.json/translate", requestContent);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Что-то пошло не так");
                }

                var responseString = await response.Content.ReadAsStringAsync();
                var yandexResponse = JsonConvert.DeserializeObject<YandexTranslateResponse>(responseString);

                return yandexResponse.Text.First();
            }
        }
    }
    class YandexTranslateResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("lang")]
        public string Language { get; set; }
        [JsonProperty("text")]
        public string[] Text { get; set; }
    }
}