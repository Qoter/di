using System.Drawing;
using TagCloud.Core.Infratructure;

namespace TagCloud.Core.Settings
{
    public class CloudSettings
    {
        public Size Size { get; }
        public double SpiralStep { get; }

        private CloudSettings(Size size, double spiralStep)
        {
            Size = size;
            SpiralStep = spiralStep;
        }

        public static Result<CloudSettings> Create(Size size, double spiralStep)
        {
            return size.Height <= 0 || size.Width <= 0 
                ? Result.Fail<CloudSettings>($"Size should be positive {size.Height}x{size.Width}")
                : spiralStep <= 0 ? Result.Fail<CloudSettings>($"Spiral step should be positibe {size.Height}x{size.Width}")
                : Result.Ok(new CloudSettings(size, spiralStep));
        }
    }
}