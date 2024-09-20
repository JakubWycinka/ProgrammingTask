namespace TextEvaluator.Domain
{
    public class TextEvaluator
    {
        private static readonly char[] WordSeparators = GetWordSeparators();

        /// <summary>
        /// During search of unique words the assumption is that any non-alphanumeric char is a separator, and any alphanumeric sign or sequence of signs are words.
        /// </summary>
        /// <param name="texts"></param>
        /// <returns></returns>
        public IDictionary<string, int> GetOccurrencesOfUniqueWords(params string[] texts)
        {
            return texts
                .SelectMany(text => text.Split(WordSeparators, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                .GroupBy(word => word, new WordComparer())
                .ToDictionary(keySelector: x => x.Key, elementSelector: y => y.Count());
        }

        private static char[] GetWordSeparators()
        {
            var nonAlphanumericChars = new List<char>() { char.MaxValue };

            for (char @char = char.MinValue; @char < char.MaxValue; @char++)
            {
                if (!char.IsLetterOrDigit(@char))
                {
                    nonAlphanumericChars.Add(@char);
                }
            }

            return nonAlphanumericChars.ToArray();
        }

        private class WordComparer : IEqualityComparer<string>
        {
            public bool Equals(string? x, string? y) => (x == null && y == null) ||
                                                        (x != null && y != null && x.Equals(y, StringComparison.OrdinalIgnoreCase));

            public int GetHashCode(string obj) => obj.GetHashCode(StringComparison.OrdinalIgnoreCase);
        }
    }
}