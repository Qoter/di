using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TagCloud.Core;
using TagCloud.Core.Interfaces;

namespace TagCloud.Console
{
    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            var txtWordsFilePath = args[0];

            var wordsProvider = new TxtFileWordsProvider(txtWordsFilePath);
            var words = wordsProvider.GetWords();

            var wordsPreprocessor = new LowerLongPreprocessor();
            var preprocessedWords = wordsPreprocessor.PreprocessWords(words);

            var statisticsCalculator = new StatisticsCalculator();
            var statistics = statisticsCalculator.CalculateStatistics(preprocessedWords);

            var styleProvider = new StyleProvider();

            var visualizator = new CloudRenderer(styleProvider);
            var bitmap = visualizator.RenderCloud(new Cloud(new CircularCloudLayouter()));

            bitmap.Save("cloud.png",  ImageFormat.Png);
        }
    }
}
