namespace TextEvaluator.Domain
{
    public class TextEvaluator : ITextEvaluator
    {
        private static readonly char[] WordSeparators = [' ', ',', '.', ':', ';', '_', '!', '?', '\t', '\n', '\r'];

        /// <summary>
        /// During search of unique words the assumption is that any non-alphanumeric char is a separator, and any alphanumeric sign or sequence of signs are words.
        /// </summary>
        /// <param name="texts">Texts that contain words e.g. "I am a method"</param>
        /// <returns>Dictionary with unique words and their counters e.g. for "I am a method" returns
        /// { "I", 1 },
        /// { "am", 1 },
        /// { "a", 1 },
        /// { "method", 1 } 
        /// </returns>
        public IReadOnlyDictionary<string, int> GetOccurrencesOfUniqueWords(params string?[]? texts)
        {
            return texts?
                .Where(text => text is not null)
                .SelectMany(text => text!.Split(WordSeparators, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                .GroupBy(word => word, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(keySelector: x => x.Key, elementSelector: y => y.Count()) ?? new Dictionary<string, int>(capacity: 0);
        }
    }
}