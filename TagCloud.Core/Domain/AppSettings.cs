using System.Drawing.Imaging;
using System.Web.Configuration;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core.Domain
{
    public class AppSettings : IWordsDirectoryProvider, ICloudSettingsProvider, IStyleSettingsProvider, IOutputSettingsProvider
    {
        public string WordsDirectory { get; set; }
        public OutputSettings OutputSettings { get; set; }
        public CloudSettings CloudSettings { get; set; }
        public StyleSettings StyleSettings { get; set; }
    }
}
