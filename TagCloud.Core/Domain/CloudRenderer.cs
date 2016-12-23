using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core.Domain
{
    public class CloudRenderer : ICloudRenderer
    {
        private readonly IStyleProvider styleProvider;
        private readonly ICloudSettingsProvider settingsProvider;
        private readonly CloudBuilder cloudBuilder;

        public CloudRenderer(
            IWordsProvider wordsProvider, 
            IStyleProvider styleProvider,
            ICloudLayouter cloudLayouter,
            ICloudSettingsProvider settingsProvider)
        {
            this.styleProvider = styleProvider;
            this.settingsProvider = settingsProvider;
            cloudBuilder = PrepareCloudBuilder(wordsProvider, styleProvider, cloudLayouter, settingsProvider);
        }

        public Bitmap Render()
        {
            var cloud = cloudBuilder.Build();
            var bitmap = new Bitmap(cloud.Size.Width, cloud.Size.Height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                graphics.FillRectangle(new SolidBrush(styleProvider.Background), 0, 0, bitmap.Width, bitmap.Height);

                foreach (var tag in cloud.Tags)
                {
                    var style = styleProvider.GetStyle(tag.Word);
                    FillString(graphics, tag.Word, style.Font, new SolidBrush(style.Color), tag.Place);
                }
            }

            return bitmap;
        }

        private static CloudBuilder PrepareCloudBuilder(
            IWordsProvider wordsProvider,
            IStyleProvider styleProvider,
            ICloudLayouter layouter,
            ICloudSettingsProvider settingsProvider)
        {
            return CloudBuilder.StartNew(layouter, settingsProvider)
                .WithWordsSize(word => TextRenderer.MeasureText(word, styleProvider.GetStyle(word).Font))
                .WithWords(wordsProvider.GetWords());
        }

        private static void FillString(Graphics g, string str, Font font, Brush brush, Rectangle place)
        {
            var currentSize = g.MeasureString(str, font);
            var placeSize = place.Size;
            var resizeFactor = Math.Min(placeSize.Height/currentSize.Height, placeSize.Width/currentSize.Width);

            var resizedFont = new Font(font.FontFamily, font.Size * resizeFactor, font.Style);

            var alignCenterFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            g.DrawString(str, resizedFont, brush, place, alignCenterFormat);
        }
    }
}
