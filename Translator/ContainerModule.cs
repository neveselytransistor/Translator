using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Translator
{
    public class ContainerModule
    {
        private readonly ServiceProvider _serviceProvider;
        public ContainerModule()
        {
            var config = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json", false, false)
                         .Build();


            _serviceProvider = new ServiceCollection()
                                  .AddSingleton<IConfiguration>(config)
                                  .AddSingleton<ITranslateService, YandexTranslateService>()
                                  .BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}