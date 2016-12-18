using System.Collections.Generic;
using System.Drawing.Imaging;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core.Domain
{
    public class CloudSaver : ICloudSaver
    {
        private readonly ICloudRenderer renderer;
        private readonly IOutputSettingsProvider outputSettingsProvider;

        private readonly Dictionary<string, ImageFormat> nameToFormat = new Dictionary<string, ImageFormat>()
        {
            ["png"] = ImageFormat.Png,
            ["jpg"] = ImageFormat.Jpeg,
            ["bmp"] = ImageFormat.Bmp
        };

        public CloudSaver(ICloudRenderer renderer, IOutputSettingsProvider outputSettingsProvider)
        {
            this.renderer = renderer;
            this.outputSettingsProvider = outputSettingsProvider;
        }

        public void Save()
        {
            var cloudBitmap = renderer.Render();
            var imageFormat = outputSettingsProvider.OutputSettings.ImageFormat;
            var filename = outputSettingsProvider.OutputSettings.OutputFilename;

            cloudBitmap.Save($"{filename}.{imageFormat}", nameToFormat[imageFormat.ToLower()]);
        }
    }
}