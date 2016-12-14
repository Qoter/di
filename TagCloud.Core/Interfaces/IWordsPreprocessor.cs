using System.Collections.Generic;

namespace TagCloud.Core.Interfaces
{
    public interface IWordsPreprocessor
    {
        IEnumerable<string> PreprocessWords(IEnumerable<string> words);
    }
}
