using System.Collections.Generic;
using TagCloud.Core.Infratructure;

namespace TagCloud.Core.Interfaces
{
    public interface IWordsProvider
    {
        Result<IEnumerable<string>> GetWords();
        Result<int> GetFrequency(string word);
    }
}
