using TagCloud.Core.Domain;

namespace TagCloud.Core.Interfaces
{
    public interface IOutputSettingsProvider
    {
        OutputSettings OutputSettings { get; }
    }
}