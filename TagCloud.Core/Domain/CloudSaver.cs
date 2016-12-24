using System.Collections.Generic;
using System.Drawing.Imaging;
using TagCloud.Core.Infratructure;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core.Domain
{
    public class CloudSaver : ICloudSaver
    {
        private readonly ICloudRenderer renderer;
        private readonly IOutputSettingsProvider outputSettingsProvider;

        public CloudSaver(ICloudRenderer renderer, IOutputSettingsProvider outputSettingsProvider)
        {
            this.renderer = renderer;
            this.outputSettingsProvider = outputSettingsProvider;
        }

        public Result<None> Save()
        {
            var cloudBitmap = renderer.Render();
            var imageFormat = outputSettingsProvider.OutputSettings.ImageFormat;
            var filename = outputSettingsProvider.OutputSettings.OutputFilename;

            var outFileName = $"{filename}.{imageFormat.ToString().ToLower()}";

            return cloudBitmap.Then(bitmap => Result.OfAction(() => bitmap.Save(outFileName, imageFormat)));
        }
    }
}