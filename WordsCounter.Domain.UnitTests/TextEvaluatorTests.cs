using FluentAssertions;

namespace TextEvaluator.Domain.UnitTests
{
    public class TextEvaluatorTests
    {
        public static TheoryData<string?[]?, Dictionary<string, int>> TestCases = new()
        {
            {
                [
                    "Go do that thing that you do so well", "I play football well"
                ],
                new Dictionary<string, int>()
                {
                    { "Go", 1 },
                    { "do", 2 },
                    { "that", 2 },
                    { "thing", 1 },
                    { "you", 1 },
                    { "so", 1 },
                    { "well", 2 },
                    { "I", 1 },
                    { "play", 1 },
                    { "football", 1 },
                }
            },
            {
                [
                    "Go do, that, thing that ,you do so well.", " I play  football well?"
                ],
                new Dictionary<string, int>()
                {
                    { "Go", 1 },
                    { "do", 2 },
                    { "that", 2 },
                    { "thing", 1 },
                    { "you", 1 },
                    { "so", 1 },
                    { "well", 2 },
                    { "I", 1 },
                    { "play", 1 },
                    { "football", 1 },
                }
            },
            {
                [
                    "Go do that thing that you do so well", "I will go play football"
                ],
                new Dictionary<string, int>()
                {
                    { "Go", 2 },
                    { "do", 2 },
                    { "that", 2 },
                    { "thing", 1 },
                    { "you", 1 },
                    { "so", 1 },
                    { "well", 1 },
                    { "I", 1 },
                    { "will", 1 },
                    { "play", 1 },
                    { "football", 1 },
                }
            },
            {
                [
                    "I am a method"
                ],
                new Dictionary<string, int>()
                {
                    { "I", 1 },
                    { "am", 1 },
                    { "a", 1 },
                    { "method", 1 },
                }
            },
            {
                [
                    null, null
                ],
                new Dictionary<string, int>() { }
            },
            {
                null,
                new Dictionary<string, int>() { }
            }
        };

        [Theory]
        [MemberData(nameof(TestCases))]
        public void GivenTwoTexts_WhenCountOccurrencesOfUniqueWords_ThenGetExpectedResult(string?[]? texts, Dictionary<string, int> expectedOccurrencesOfUniqueWords)
        {
            // Arrange
            var wordsCounter = new TextEvaluator();

            // Act
            var result = wordsCounter.GetOccurrencesOfUniqueWords(texts);

            // Assert
            result.Should().BeEquivalentTo(expectedOccurrencesOfUniqueWords);
        }
    }
}