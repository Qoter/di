using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.Core.Infratructure;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core.Domain
{
    public class WordsProvider : IWordsProvider
    {
        private readonly Dictionary<string, int> wordToFrequency;

        public WordsProvider(IInputSettingsProvider inputSettingsProvider, IWordsPreprocessor preprocessor)
        {
            wordToFrequency = ReadWords(inputSettingsProvider, preprocessor);
        }

        public IEnumerable<string> GetWords()
        {
            return wordToFrequency.Keys;
        }

        public Result<int> GetFrequency(string word)
        {
            return !wordToFrequency.ContainsKey(word) 
                ? Result.Fail<int>($"Internal error. Not found frequency for word {word}") 
                : Result.Ok(wordToFrequency[word]);
        }

        private static Dictionary<string, int> ReadWords(IInputSettingsProvider inputSettingsProvider, IWordsPreprocessor preprocessor)
        {
            var words = File.ReadLines(inputSettingsProvider.InputSettings.WordsFile);

            return preprocessor
                .PreprocessWords(words)
                .GroupBy(word => word)
                .ToDictionary(wordGroup => wordGroup.Key, wordGroup => wordGroup.Count());
        }
    }
}