using System.Drawing;
using TagCloud.Core.Interfaces;
using TagCloud.Core.Primitives;

namespace TagCloud.Core
{
    public class FrequencyStyleProvider : IStyleProvider
    {
        private readonly IWordsProvider wordsProvider;

        private const float BaseFontSize = 10;

        public FrequencyStyleProvider(IWordsProvider wordsProvider)
        {
            this.wordsProvider = wordsProvider;
        }

        public Color BackgroundColor { get; } = Color.White;
        public Style GetStyle(string word)
        {
            var fontSize = BaseFontSize*wordsProvider.GetFrequency(word);
            var font = new Font(FontFamily.GenericMonospace, fontSize);

            return new Style(font, Color.DimGray);
        }
    }
}
