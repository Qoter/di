using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.Core.Infratructure;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core.Domain
{
    public class WordsProvider : IWordsProvider
    {
        private readonly Result<Dictionary<string, int>> wordToFrequencyResult;

        public WordsProvider(IInputSettingsProvider inputSettingsProvider, IWordsPreprocessor preprocessor)
        {
            wordToFrequencyResult = ReadWords(inputSettingsProvider, preprocessor);
        }

        public Result<IEnumerable<string>> GetWords()
        {
            return wordToFrequencyResult.Then(freqDict => (IEnumerable<string>)freqDict.Keys);
        }

        public Result<int> GetFrequency(string word)
        {
            return wordToFrequencyResult
                .Then(wordToFrequency => wordToFrequency.ContainsKey(word)
                    ? Result.Ok(wordToFrequency[word])
                    : Result.Fail<int>($"Internal error.Not found frequency for word {word}"));
        }

        private static Result<Dictionary<string, int>> ReadWords(IInputSettingsProvider inputSettingsProvider, IWordsPreprocessor preprocessor)
        {
            return Result.Of(() => File.ReadAllLines(inputSettingsProvider.InputSettings.WordsFile))
                .Then(words => preprocessor
                    .PreprocessWords(words)
                    .GroupBy(word => word)
                    .ToDictionary(wordGroup => wordGroup.Key, wordGroup => wordGroup.Count()));
        }
    }
}