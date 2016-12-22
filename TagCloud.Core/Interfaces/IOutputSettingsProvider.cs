using TagCloud.Core.Domain;
using TagCloud.Core.Settings;

namespace TagCloud.Core.Interfaces
{
    public interface IOutputSettingsProvider
    {
        OutputSettings OutputSettings { get; }
    }
}