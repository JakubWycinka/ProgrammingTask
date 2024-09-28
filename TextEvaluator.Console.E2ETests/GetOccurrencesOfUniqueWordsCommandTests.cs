using FluentAssertions;

namespace TextEvaluator.Console.E2ETests
{
    public class GetOccurrencesOfUniqueWordsCommandTests
    {
        private const string TestFilesFolderPrefix = "TestFiles";
        private const string FootballTextFilePath = $"{TestFilesFolderPrefix}/football.txt";
        private const string DoThingTextFilePath = $"{TestFilesFolderPrefix}/do_so_well.txt";
        private readonly TextEvaluatorRunner _consoleAppRunner = new();

        [Fact]
        public async Task GivenOneFile_WhenGetOccurrencesOfUniqueWordsCommand_Returns()
        {
            // Arrange
            var filePaths = new[] { FootballTextFilePath };

            // Act
            var (standardOutput, standardError) = await _consoleAppRunner.GetOccurrencesOfUniqueWordsConsoleOutput(filePaths);

            // Assert
            standardError.Should().BeEmpty();
            standardOutput.Should().Contain("""
                                            {
                                              "I": 1,
                                              "play": 1,
                                              "football": 1,
                                              "well": 1
                                            }
                                            """);
        }

        [Fact]
        public async Task GivenTwoFiles_WhenGetOccurrencesOfUniqueWordsCommand_Returns()
        {
            // Arrange
            var filePaths = new[] { DoThingTextFilePath, FootballTextFilePath };

            // Act
            var (standardOutput, standardError) = await _consoleAppRunner.GetOccurrencesOfUniqueWordsConsoleOutput(filePaths);

            // Assert
            standardError.Should().BeEmpty();
            standardOutput.Should().Contain("""
                                            {
                                              "Go": 1,
                                              "do": 2,
                                              "that": 2,
                                              "thing": 1,
                                              "you": 1,
                                              "so": 1,
                                              "well": 2,
                                              "I": 1,
                                              "play": 1,
                                              "football": 1
                                            }
                                            """);
        }
    }
}