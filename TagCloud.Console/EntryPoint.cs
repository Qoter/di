using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TagCloud.Core;

namespace TagCloud.Console
{
    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            var txtWordsFilePath = args[0];
            var backgroundColor = args[1];
            var fontColor = args[2];

            var wordsProvider = new TxtFileWordsProvider(txtWordsFilePath);
            var words = wordsProvider.GetWords();

            var wordsPreprocessor = new LowerLongPreprocessor();
            var preprocessedWords = wordsPreprocessor.PreprocessWords(words);

            var statisticsCalculator = new StatisticsCalculator();
            var statistics = statisticsCalculator.CalculateStatistics(preprocessedWords);

            var visualizator = new CloudRenderer(8f, ColorTranslator.FromHtml(backgroundColor), ColorTranslator.FromHtml(fontColor));
            var bitmap = visualizator.RenderCloud(statistics);

            bitmap.Save("cloud.png",  ImageFormat.Png);
        }
    }
}
