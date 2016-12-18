using TagCloud.Core.Domain;

namespace TagCloud.Core.Interfaces
{
    public interface IStyleSettingsProvider
    {
        StyleSettings StyleSettings { get; }
    }
}