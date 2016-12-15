using System.Collections.Generic;

namespace TagCloud.Core.Interfaces
{
    public interface IStatisticsCalculator
    {
        IEnumerable<KeyValuePair<string, int>> CalculateStatistics(IEnumerable<string> words);
    }
}