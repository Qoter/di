using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core
{
    public class WordsProvider : IWordsProvider
    {
        private readonly Dictionary<string, int> wordToEntryCount;

        public WordsProvider(string fileName, IWordsPreprocessor preprocessor)
        {
            wordToEntryCount = PrepareWords(fileName, preprocessor);
        }

        public IEnumerable<string> GetWords()
        {
            return wordToEntryCount.Keys;
        }

        public int GetWordFactor(string word)
        {
            if (!wordToEntryCount.ContainsKey(word))
                throw new ArgumentException($"Word: {word} not found");

            return wordToEntryCount[word];
        }

        private static Dictionary<string, int> PrepareWords(string fileName, IWordsPreprocessor preprocessor)
        {
            var words = File.ReadLines(fileName);

            return preprocessor
                .PreprocessWords(words)
                .GroupBy(word => word)
                .ToDictionary(wordGroup => wordGroup.Key, wordGroup => wordGroup.Count());
        }
    }
}