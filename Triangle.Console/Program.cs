using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;

namespace Triangle.Console
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceProvider = CreateServiceProvider();

            var rootCommand = new RootCommand();
            rootCommand.AddCommand(serviceProvider.GetRequiredService<GetTriangleKindDueToItsSidesCommand>());
            await rootCommand.InvokeAsync(args);
        }

        private static ServiceProvider CreateServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<GetTriangleKindDueToItsSidesCommand>();
            serviceCollection.AddLogging();
            serviceCollection.AddKeyedSingleton<TextWriter>("standardOutput", System.Console.Out);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}