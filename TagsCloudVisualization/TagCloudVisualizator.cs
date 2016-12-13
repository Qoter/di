using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace TagsCloudVisualization
{
    public class TagCloudVisualizator
    {
        private readonly Color backgroundColor;
        private readonly Color fontColor;
        private readonly float baseFontSize;

        public TagCloudVisualizator(float baseFontSize, Color backgroundColor, Color fontColor)
        {
            this.baseFontSize = baseFontSize;
            this.backgroundColor = backgroundColor;
            this.fontColor = fontColor;
        }

        public Bitmap CreateCloud(Dictionary<string, int> wordStatisics)
        {
            var wordsStyleList = GenerateWordsStyle(wordStatisics).ToList();
            var layouter = CreateNormalizeLayouter(wordsStyleList);

            return CreateBitmap(wordsStyleList, layouter);
        }

        private static CircularCloudLayouter CreateNormalizeLayouter(IEnumerable<Tuple<string, Font, Color>> wordsStyle)
        {
            var layouter = new CircularCloudLayouter();
            layouter.PutAllRectangles(wordsStyle.Select(wordStyle => TextRenderer.MeasureText(wordStyle.Item1, wordStyle.Item2)));
            layouter.Normalize();
            return layouter;
        }

        private IEnumerable<Tuple<string, Font, Color>> GenerateWordsStyle(Dictionary<string, int> statistics)
        {
            return statistics.Keys.Select(word => Tuple.Create(word, CalculateFont(statistics[word]), CalculateColor(statistics[word])));
        }

        private Bitmap CreateBitmap(List<Tuple<string, Font, Color>> wordsStyleList, CircularCloudLayouter layouter)
        {
            var imageSize = layouter.CalculateSize();
            var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                graphics.FillRectangle(new SolidBrush(backgroundColor), 0, 0, bitmap.Width, bitmap.Height);
                for (var i = 0; i < wordsStyleList.Count; i++)
                {
                    var word = wordsStyleList[i].Item1;
                    var font = wordsStyleList[i].Item2;
                    var color = wordsStyleList[i].Item3;
                    graphics.DrawString(word, font, new SolidBrush(color), layouter.Rectangles[i]);
                }
            }

            return bitmap;
        }

        private Font CalculateFont(int wordStatistic)
        {
            var fontSize = baseFontSize * (float)Math.Sqrt(wordStatistic);
            return new Font("Segoe UI", fontSize);
        }

        private Color CalculateColor(int wordStatistic)
        {
            return fontColor;
        }
    }
}
