using System;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using TagCloud.Core.Infratructure;
using TagCloud.Core.Interfaces;
using TagCloud.Core.Primitives;

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

        public Result<Bitmap> Render()
        {
            return cloudBuilder.Build().Then(RednderCloud);
        }

        private Result<Bitmap> RednderCloud(Cloud cloud)
        {
            var bitmap = new Bitmap(cloud.Size.Width, cloud.Size.Height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                graphics.FillRectangle(new SolidBrush(styleProvider.Background), 0, 0, bitmap.Width, bitmap.Height);

                return DrawTags(graphics, cloud).Then(_ => bitmap);
            }
        }

        private Result<None> DrawTags(Graphics g, Cloud cloud)
        {
            foreach (var tag in cloud.Tags)
            {
                var fillResut = styleProvider.GetStyle(tag.Word)
                    .Then(style => FillString(g, tag.Word, style.Font, new SolidBrush(style.Color), tag.Place));

                if (!fillResut.IsSuccess)
                {
                    return Result.Fail<None>(fillResut.Error);
                }
            }

            return new Result<None>();
        }

        private static CloudBuilder PrepareCloudBuilder(
            IWordsProvider wordsProvider,
            IStyleProvider styleProvider,
            ICloudLayouter layouter,
            ICloudSettingsProvider settingsProvider)
        {
            return CloudBuilder.StartNew(layouter, settingsProvider)
                .WithWordsSize(word => styleProvider.GetStyle(word).Then(style => TextRenderer.MeasureText(word, style.Font)))
                .WithWords(wordsProvider.GetWords());
        }

        private static Result<None> FillString(Graphics g, string word, Font font, Brush brush, Rectangle place)
        {
            var currentSize = g.MeasureString(word, font);
            var placeSize = place.Size;
            var resizeFactor = Math.Min(placeSize.Height/currentSize.Height, placeSize.Width/currentSize.Width);

            var resizedFont = new Font(font.FontFamily, font.Size * resizeFactor, font.Style);

            var alignCenterFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            if (resizedFont.Size < 4)
            {
                return Result.Fail<None>($"Word: {word} does not fit on image");
            }

            g.DrawString(word, resizedFont, brush, place, alignCenterFormat);
            return new Result<None>();
        }
    }
}
