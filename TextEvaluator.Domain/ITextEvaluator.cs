namespace TextEvaluator.Domain
{
    public interface ITextEvaluator
    {
        public IReadOnlyDictionary<string, int> GetOccurrencesOfUniqueWords(params string?[]? texts);
    }
}