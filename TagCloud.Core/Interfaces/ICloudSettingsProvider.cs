using TagCloud.Core.Domain;
using TagCloud.Core.Settings;

namespace TagCloud.Core.Interfaces
{
    public interface ICloudSettingsProvider
    {
        CloudSettings CloudSettings { get; }
    }
}
