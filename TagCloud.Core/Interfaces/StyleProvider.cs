using System.Drawing;

namespace TagCloud.Core.Interfaces
{
    public class StyleProvider : IStyleProvider
    {
        public Color BackgroundColor { get; }
        public Style GetItemStyle(CloudItem item)
        {
            return null;
        }
    }
}
