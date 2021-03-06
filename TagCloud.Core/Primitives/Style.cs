using System.Drawing;

namespace TagCloud.Core.Primitives
{
    public class Style
    {
        public readonly Color Color;
        public readonly Font Font;

        public Style(Font font, Color color)
        {
            Color = color;
            Font = font;
        }
    }
}