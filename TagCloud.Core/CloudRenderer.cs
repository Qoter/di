using System.Drawing;
using TagCloud.Core.Interfaces;

namespace TagCloud.Core
{
    public class CloudRenderer
    {
        private readonly IWordsProvider wordsProvider;
        private readonly IStyleProvider styleProvider;
        private readonly TagLayouter tagLayouter;

        public CloudRenderer(IWordsProvider wordsProvider, IStyleProvider styleProvider, TagLayouter tagLayouter)
        {
            this.wordsProvider = wordsProvider;
            this.styleProvider = styleProvider;
            this.tagLayouter = tagLayouter;
        }

        public Bitmap Render()
        {
            var bitmap = new Bitmap(0, 0);

            //using (var graphics = Graphics.FromImage(bitmap))
            //{
            //    graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            //    graphics.FillRectangle(new SolidBrush(styleProvider.BackgroundColor), 0, 0, bitmap.Width, bitmap.Height);

            //    foreach (var item in TagCloud.GetItems())
            //    {
            //        var wordStyle = styleProvider.GetWordStyle(item);
            //        graphics.DrawString(item.Word, wordStyle.Font, new SolidBrush(wordStyle.Color), item.Place);
            //    }
            //}

            return bitmap;
        }
    }
}
