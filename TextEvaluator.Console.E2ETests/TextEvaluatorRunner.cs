using System.Diagnostics;
using System.Reflection;

namespace TextEvaluator.Console.E2ETests
{
    public class TextEvaluatorRunner
    {
        private static readonly string TextEvaluatorConsoleExePath = Assembly.GetAssembly(typeof(TextEvaluator.Console.Program))!.Location.Replace(".dll", ".exe");

        public async Task<(string StandardOutput, string StandardError)> GetOccurrencesOfUniqueWordsConsoleOutput(string?[]? filePaths, CancellationToken cancellationToken = default)
        {
            var filePathArguments = filePaths is null
                ? string.Empty
                : string.Join(' ', filePaths.Select(filePath => $"\"{filePath}\""));

            var arguments = $"{GetOccurrencesOfUniqueWordsCommand.CommandName} {filePathArguments}";
            return await RunAndCaptureConsoleOutput(arguments, cancellationToken);
        }

        public async Task<(string StandardOutput, string StandardError)> RunAndCaptureConsoleOutput(string arguments, CancellationToken cancellationToken = default)
        {
            using var process = new Process();
            process.StartInfo.FileName = TextEvaluatorConsoleExePath;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            var results = await Task.WhenAll(
                process.StandardOutput.ReadToEndAsync(cancellationToken),
                process.StandardError.ReadToEndAsync(cancellationToken));
            return (results[0], results[1]);
        }
    }
}