using System.Drawing;

namespace TagCloud.Core.Interfaces
{
    public interface IStyleProvider
    {
        Color BackgroundColor { get; }
        Style GetItemStyle(CloudItem item);
    }
}