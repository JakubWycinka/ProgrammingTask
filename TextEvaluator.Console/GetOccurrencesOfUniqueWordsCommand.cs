using System.CommandLine;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TextEvaluator.Domain;

namespace TextEvaluator.Console
{
    public class GetOccurrencesOfUniqueWordsCommand : Command
    {
        public const string CommandName = "getOccurrencesOfUniqueWords";
        private readonly ITextEvaluator _textEvaluator;
        private readonly TextWriter _standardOutput;
        private readonly ILogger<GetOccurrencesOfUniqueWordsCommand> _logger;

        public GetOccurrencesOfUniqueWordsCommand(
            ITextEvaluator textEvaluator,
            [FromKeyedServices("standardOutput")] TextWriter standardOutput,
            ILogger<GetOccurrencesOfUniqueWordsCommand> logger)
            : base(name: CommandName,
                description: $$"""
                               Loads text files and prints occurrences of unique words.
                               During search of unique words the assumption is that any non-alphanumeric char is a separator, and any alphanumeric sign or sequence of signs are words.
                               e.g. for "I am a method" returns
                                     {
                                       "I": 1,
                                       "am": 1,
                                       "a": 1,
                                       "method": 1
                                     }
                               Example command:
                               {{CommandName}} "C:\text1.txt" "C:\text2.txt"
                               """)
        {
            _textEvaluator = textEvaluator;
            _standardOutput = standardOutput;
            _logger = logger;
            var inputCommandArgument = new Argument<string?[]?>("filePaths");
            AddArgument(inputCommandArgument);
            this.SetHandler(Handle, inputCommandArgument);
        }

        private async Task Handle(string?[]? filePaths)
        {
            try
            {
                var readFileContentsTasks = filePaths?
                    .Where(filePath => filePath != null)
                    .Select(filePath => File.ReadAllTextAsync(filePath!)) ?? Enumerable.Empty<Task<string>>();

                var fileContents = await Task.WhenAll(readFileContentsTasks);

                var occurrencesOfUniqueWords = _textEvaluator.GetOccurrencesOfUniqueWords(fileContents);

                var occurrencesOfUniqueWordsConsoleString = JsonSerializer.Serialize(occurrencesOfUniqueWords,
                    new JsonSerializerOptions()
                    {
                        WriteIndented = true,
                    });

                await _standardOutput.WriteLineAsync("Occurrences of unique words:" + Environment.NewLine + occurrencesOfUniqueWordsConsoleString);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to execute program.");
                await _standardOutput.WriteLineAsync("Failed to execute program." + Environment.NewLine + e.Message);
            }
        }
    }
}