using System.Drawing;

namespace TagCloud.Core
{
    public class Tag
    {
        public readonly string Word;
        public readonly Rectangle Place;

        public Tag(string word, Rectangle place)
        {
            Word = word;
            Place = place;
        }
    }
}
