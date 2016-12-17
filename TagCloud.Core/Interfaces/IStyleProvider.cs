using TagCloud.Core.Primitives;

namespace TagCloud.Core.Interfaces
{
    public interface IStyleProvider
    {
        Style GetStyle(string word);
    }
}