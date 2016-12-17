using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core.Model
{
    public class LowerLongPreprocessor : IWordsPreprocessor
    {
        public IEnumerable<string> PreprocessWords(IEnumerable<string> words)
        {
            return words
                .Where(word => word.Length > 3)
                .Select(word => word.ToLower());
        }
    }
}
