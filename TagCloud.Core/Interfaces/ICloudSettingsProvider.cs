using TagCloud.Core.Domain;

namespace TagCloud.Core.Interfaces
{
    public interface ICloudSettingsProvider
    {
        CloudSettings CloudSettings { get; }
    }
}
