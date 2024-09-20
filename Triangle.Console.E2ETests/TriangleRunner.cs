using System.Diagnostics;
using System.Reflection;

namespace Triangle.Console.E2ETests
{
    public class TriangleRunner
    {
        private static readonly string TextEvaluatorConsoleExePath = Assembly.GetAssembly(typeof(Triangle.Console.Program))!.Location.Replace(".dll", ".exe");

        public async Task<(string StandardOutput, string StandardError)> GetTriangleKindDueToItsSidesConsoleOutput(int side1, int side2, int side3, CancellationToken cancellationToken = default)
        {
            var arguments = $"{GetTriangleKindDueToItsSidesCommand.CommandName} {side1} {side2} {side3}";
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