using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core.Domain
{
    public class WordsProvider : IWordsProvider
    {
        private readonly Dictionary<string, int> wordToFrequency;

        public WordsProvider(IWordsDirectoryProvider wordsDirectoryProvider, IWordsPreprocessor preprocessor)
        {
            wordToFrequency = ReadWords(wordsDirectoryProvider, preprocessor);
        }

        public IEnumerable<string> GetWords()
        {
            return wordToFrequency.Keys;
        }

        public int GetFrequency(string word)
        {
            if (!wordToFrequency.ContainsKey(word))
                throw new ArgumentException($"Word: {word} not found");

            return wordToFrequency[word];
        }

        private static Dictionary<string, int> ReadWords(IWordsDirectoryProvider wordsDirectoryProvider, IWordsPreprocessor preprocessor)
        {
            var words = File.ReadLines(wordsDirectoryProvider.WordsDirectory);

            return preprocessor
                .PreprocessWords(words)
                .GroupBy(word => word)
                .ToDictionary(wordGroup => wordGroup.Key, wordGroup => wordGroup.Count());
        }
    }
}