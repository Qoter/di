using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core
{
    public class CloudRenderer
    {
        private readonly IStyleProvider styleProvider;
        private readonly CloudBuilder cloudBuilder;

        public CloudRenderer(IWordsProvider wordsProvider, IStyleProvider styleProvider, ICloudLayouter cloudLayouter)
        {
            this.styleProvider = styleProvider;
            cloudBuilder = PrepareCloudBuilder(wordsProvider, styleProvider, cloudLayouter);
        }

        private static CloudBuilder PrepareCloudBuilder(IWordsProvider wordsProvider, IStyleProvider styleProvider, ICloudLayouter layouter)
        {
            return CloudBuilder.StartNew(layouter)
                .WithWordsSize(word => TextRenderer.MeasureText(word, styleProvider.GetStyle(word).Font))
                .WithWords(wordsProvider.GetWords());
        }

        private Bitmap Render(TagCloud cloud)
        {
            var bitmap = new Bitmap(cloud.Size.Width, cloud.Size.Height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                graphics.FillRectangle(new SolidBrush(styleProvider.BackgroundColor), 0, 0, bitmap.Width, bitmap.Height);

                foreach (var tag in cloud.Tags)
                {
                    var style = styleProvider.GetStyle(tag.Word);
                    var strFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    graphics.DrawString(tag.Word, style.Font, new SolidBrush(style.Color), tag.Place, strFormat);
                }
            }

            return bitmap;
        }

        public Bitmap Render(Size cloudSize)
        {
            return Render(cloudBuilder.Build(cloudSize));
        }

        public Bitmap Render()
        {
            return Render(cloudBuilder.Build());
        }
    }
}
