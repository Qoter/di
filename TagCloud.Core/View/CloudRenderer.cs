﻿using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using TagCloud.Core.Interfaces;
using TagCloud.Core.Model;

namespace TagCloud.Core.View
{
    public class CloudRenderer
    {
        private readonly IStyleProvider styleProvider;
        private readonly CloudSettings settings;
        private readonly CloudBuilder cloudBuilder;

        public CloudRenderer(IWordsProvider wordsProvider, IStyleProvider styleProvider, ICloudLayouter cloudLayouter, CloudSettings settings)
        {
            this.styleProvider = styleProvider;
            this.settings = settings;
            cloudBuilder = PrepareCloudBuilder(wordsProvider, styleProvider, cloudLayouter);
        }

        public Bitmap Render()
        {
            var cloud = cloudBuilder.Build(settings.Size);
            var bitmap = new Bitmap(settings.Size.Width, settings.Size.Height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                graphics.FillRectangle(new SolidBrush(settings.BackgroundColor), 0, 0, bitmap.Width, bitmap.Height);

                foreach (var tag in cloud.Tags)
                {
                    var style = styleProvider.GetStyle(tag.Word);
                    FillString(graphics, tag.Word, style.Font, new SolidBrush(style.Color), tag.Place);
                }
            }

            return bitmap;
        }

        private static CloudBuilder PrepareCloudBuilder(IWordsProvider wordsProvider, IStyleProvider styleProvider, ICloudLayouter layouter)
        {
            return CloudBuilder.StartNew(layouter)
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
