using System.Drawing;

namespace TagCloud.Core
{
    public class CloudItem
    {
        public readonly string Word;
        public readonly Rectangle Area;

        public CloudItem(string word, Rectangle area)
        {
            Word = word;
            Area = area;
        }
    }
}