using System.Drawing;
using TagCloud.Core.Primitives;

namespace TagCloud.Core.Interfaces
{
    public interface IStyleProvider
    {
        Color BackgroundColor { get; }
        Style GetStyle(string word);
    }
}