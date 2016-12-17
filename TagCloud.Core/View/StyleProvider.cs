using System.Drawing;
using TagCloud.Core.Interfaces;
using TagCloud.Core.Primitives;

namespace TagCloud.Core.View
{
    public class StyleProvider : IStyleProvider
    {
        private readonly IWordsProvider wordsProvider;
        private readonly CloudSettings settings;

        private const float BaseFontSize = 10;

        public StyleProvider(IWordsProvider wordsProvider, CloudSettings settings)
        {
            this.wordsProvider = wordsProvider;
            this.settings = settings;
        }

        public Style GetStyle(string word)
        {
            var fontSize = BaseFontSize*wordsProvider.GetFrequency(word);
            var font = new Font(settings.FontFamily, fontSize);
            return new Style(font, settings.FontColor);
        }
    }
}
