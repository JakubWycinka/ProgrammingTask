using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TextEvaluator.Domain;

namespace TextEvaluator.Console
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceProvider = CreateServiceProvider();

            var rootCommand = new RootCommand();
            rootCommand.AddCommand(serviceProvider.GetRequiredService<GetOccurrencesOfUniqueWordsCommand>());
            await rootCommand.InvokeAsync(args);
        }

        private static ServiceProvider CreateServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<ITextEvaluator, Domain.TextEvaluator>();
            serviceCollection.AddTransient<GetOccurrencesOfUniqueWordsCommand>();
            serviceCollection.AddLogging();
            serviceCollection.AddKeyedSingleton<TextWriter>("standardOutput", System.Console.Out);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}