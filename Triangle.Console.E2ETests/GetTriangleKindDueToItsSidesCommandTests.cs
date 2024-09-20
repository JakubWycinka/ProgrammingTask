using FluentAssertions;
using Triangle.Domain;

namespace Triangle.Console.E2ETests
{
    public class GetTriangleKindDueToItsSidesCommandTests
    {
        [Fact]
        public async Task GivenEqualTriangleSides_WhenGetTriangleKindDueToItsSidesCommand_ReturnsEquilateral()
        {
            // Arrange
            var consoleAppRunner = new TriangleRunner();

            // Act
            var (standardOutput, standardError) = await consoleAppRunner.GetTriangleKindDueToItsSidesConsoleOutput(
                side1: 5,
                side2: 5,
                side3: 5);

            // Assert
            standardError.Should().BeEmpty();
            standardOutput.Should().Contain(TriangleKindDueToItsSides.Equilateral.ToString());
        }
    }
}