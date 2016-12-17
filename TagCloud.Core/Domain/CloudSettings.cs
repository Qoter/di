using System.Drawing;

namespace TagCloud.Core.Domain
{
    public class CloudSettings
    {
        public Color BackgroundColor { get; set; }
        public Color FontColor { get; set; }
        public FontFamily FontFamily { get; set; }
        public Size Size { get; set; }
        public double SpiralFactor { get; set; }
    }
}