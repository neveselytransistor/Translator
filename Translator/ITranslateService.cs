using System.Threading.Tasks;

namespace Translator
{
    public interface ITranslateService
    {
        Task<string> TranslateAsync(string input, string language);
    }
}