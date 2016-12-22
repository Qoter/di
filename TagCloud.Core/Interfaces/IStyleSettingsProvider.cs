using TagCloud.Core.Domain;
using TagCloud.Core.Settings;

namespace TagCloud.Core.Interfaces
{
    public interface IStyleSettingsProvider
    {
        StyleSettings StyleSettings { get; }
    }
}