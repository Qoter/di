using TagCloud.Core.Interfaces;

namespace TagCloud.Core.Settings
{
    public class AppSettings : IInputSettingsProvider, IOutputSettingsProvider, ICloudSettingsProvider, IStyleSettingsProvider
    {
        public InputSettings InputSettings { get; set; }
        public OutputSettings OutputSettings { get; set; }
        public CloudSettings CloudSettings { get; set; }
        public StyleSettings StyleSettings { get; set; }
    }
}
