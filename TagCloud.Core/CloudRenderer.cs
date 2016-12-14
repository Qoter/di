using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core
{
    public class CloudRenderer
    {
        private readonly IStyleProvider styleProvider;

        public CloudRenderer(IStyleProvider styleProvider)
        {
            this.styleProvider = styleProvider;
        }

        public Bitmap RenderCloud(Cloud cloud)
        {
            var size = cloud.CalculateSize();
            var bitmap = new Bitmap(size.Width, size.Height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                graphics.FillRectangle(new SolidBrush(styleProvider.BackgroundColor), 0, 0, bitmap.Width, bitmap.Height);

                foreach (var item in cloud.GetItems())
                {
                    var wordStyle = styleProvider.GetItemStyle(item);
                    graphics.DrawString(item.Word, wordStyle.Font, new SolidBrush(wordStyle.Color), item.Area);
                }
            }

            return bitmap;
        }
    }
}
