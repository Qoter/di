using System.Collections.Generic;

namespace TagCloud.Core.Interfaces
{
    public interface IWordsProvider
    {
        IEnumerable<string> GetWords();
        int GetWordFactor(string word);
    }
}
