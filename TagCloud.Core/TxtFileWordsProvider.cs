using System.Collections.Generic;
using System.IO;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core
{
    public class TxtFileWordsProvider : IWordsProvider
    {
        private readonly string fileName;

        public TxtFileWordsProvider(string fileName)
        {
            this.fileName = fileName;
        }

        public IEnumerable<string> GetWords()
        {
            return File.ReadLines(fileName);
        }
    }
}