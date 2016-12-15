using System.Drawing;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core
{
    public class DefaultStyleProvider : IStyleProvider
    {
        public Color BackgroundColor { get; } = Color.White;
        public Style GetStyle(string word)
        {
            return new Style(new Font(FontFamily.GenericMonospace, 8f), Color.DimGray);
        }
    }
}
