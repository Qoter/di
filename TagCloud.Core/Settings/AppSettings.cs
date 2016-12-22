using TagCloud.Core.Domain;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core.Settings
{
    public class AppSettings : IWordsDirectoryProvider, ICloudSettingsProvider, IStyleSettingsProvider, IOutputSettingsProvider
    {
        public string WordsDirectory { get; set; }
        public OutputSettings OutputSettings { get; set; }
        public CloudSettings CloudSettings { get; set; }
        public StyleSettings StyleSettings { get; set; }
    }
}
