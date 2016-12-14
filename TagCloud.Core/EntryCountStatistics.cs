using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core
{
    public class StatisticsCalculator : IStatisticsCalculator
    {
        public IEnumerable<KeyValuePair<string, int>> CalculateStatistics(IEnumerable<string> words)
        {
            return words
                .GroupBy(word => word)
                .Select(wordGroup => new KeyValuePair<string, int>(wordGroup.Key, wordGroup.Count()));
        }
    }
}
