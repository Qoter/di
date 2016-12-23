using System.Collections.Generic;
using System.Drawing.Imaging;
using TagCloud.Core.Infratructure;

namespace TagCloud.Core.Settings
{
    public class OutputSettings
    {
        public ImageFormat ImageFormat { get; }
        public string OutputFilename { get; }

        private static readonly Dictionary<string, ImageFormat> formats = new Dictionary<string, ImageFormat>
        {
            ["png"] = ImageFormat.Png,
            ["jpg"] = ImageFormat.Jpeg,
            ["bmp"] = ImageFormat.Bmp
        };

        public OutputSettings(ImageFormat imageFormat, string outputFilename)
        {
            ImageFormat = imageFormat;
            OutputFilename = outputFilename;
        }

        public static Result<OutputSettings> FromStrings(string imageFormat, string outputFilename)
        {
            return !formats.ContainsKey(imageFormat) 
                ? Result.Fail<OutputSettings>($"Not supported image format {imageFormat}") 
                : Result.Ok(new OutputSettings(formats[imageFormat], outputFilename));
        }
    }
}