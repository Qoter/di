using System.Drawing;
using TagCloud.Core.Infratructure;
using TagCloud.Core.Primitives;

namespace TagCloud.Core.Interfaces
{
    public interface IStyleProvider
    {
        Color Background { get; }
        Result<Style> GetStyle(string word);
    }
}