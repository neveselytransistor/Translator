using System;
using System.Threading.Tasks;

namespace Translator
{
    class Program
    {
        static void Main(string[] args)
        {   
            RunAsync().GetAwaiter().GetResult();  
        }

        static async Task RunAsync()
        {
            string text;
            do
            {
                Console.Write("Введите текст для перевода:> ");
                text = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(text));

            Console.WriteLine();

           var containerModule = new ContainerModule();

            var translateService = containerModule.GetService<ITranslateService>();
            try
            {
                var translatedText = await translateService.TranslateAsync(text, "ru-en");

                Console.WriteLine(translatedText);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("\nНажмите любую клавишу...");
                Console.ReadKey(true);
            }
        }
    }
}