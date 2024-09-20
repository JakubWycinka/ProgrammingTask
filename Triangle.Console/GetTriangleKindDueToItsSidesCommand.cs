using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.CommandLine;

namespace Triangle.Console
{
    public class GetTriangleKindDueToItsSidesCommand : Command
    {
        public const string CommandName = "getTriangleKindDueToItsSides";
        private readonly TextWriter _standardOutput;
        private readonly ILogger<GetTriangleKindDueToItsSidesCommand> _logger;

        public GetTriangleKindDueToItsSidesCommand(
            [FromKeyedServices("standardOutput")] TextWriter standardOutput,
            ILogger<GetTriangleKindDueToItsSidesCommand> logger)
            : base(name: CommandName,
                description: $"""
                               Returns triangle kind due to its sides.
                               For arguments 5 5 5 returns {Domain.TriangleKindDueToItsSides.Equilateral.ToString()}.
                               Example command:
                               {CommandName} 5 5 5
                               """)
        {
            _standardOutput = standardOutput;
            _logger = logger;

            var triangleSide1Argument = new Argument<int>(
                name: "side1",
                description: "One of triangle's side.");
            var triangleSide2Argument = new Argument<int>(
                name: "side2",
                description: "One of triangle's side.");
            var triangleSide3Argument = new Argument<int>(
                name: "side3",
                description: "One of triangle's side.");

            AddArgument(triangleSide1Argument);
            AddArgument(triangleSide2Argument);
            AddArgument(triangleSide3Argument);

            this.SetHandler(Handle, triangleSide1Argument, triangleSide2Argument, triangleSide3Argument);
        }

        private async Task Handle(int side1, int side2, int side3)
        {
            try
            {
                var triangle = new Domain.Triangle(side1, side2, side3);

                var triangleKindDueToTheSidesConsoleString = triangle.KindDueToTheSides.ToString();

                await _standardOutput.WriteLineAsync("Triangle kind due to the sides:" + Environment.NewLine + triangleKindDueToTheSidesConsoleString);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to execute program.");
                await _standardOutput.WriteLineAsync("Failed to execute program." + Environment.NewLine + e.Message);
            }
        }
    }
}