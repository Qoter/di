using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TagCloud.Core;

namespace TagCloud.Console
{
    public class Program
    {
        public static readonly Regex WordRegex = new Regex(@"[a-z]+", RegexOptions.Compiled);
        public static void Main(string[] args)
        {
            var textPath = args[0];
            var backgroundColor = args[1];
            var fontColor = args[2];

            var visualizator = new TagCloudVisualizator(8f, ColorTranslator.FromHtml(backgroundColor), ColorTranslator.FromHtml(fontColor));

            var text = File.ReadAllText(textPath);
            var statistics = CreateStatistics(text);

            var bitmap = visualizator.CreateCloud(statistics);

            bitmap.Save("cloud.png",  ImageFormat.Png);
        }

        private static Dictionary<string, int> CreateStatistics(string text)
        {
            return WordRegex.Matches(text.ToLower()).EnumerateMatches()
                .Where(word => word.Length > 3)
                .GroupBy(word => word)
                .ToDictionary(group => group.Key, group => group.Count());
        }
    }
}
