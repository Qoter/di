using System.Drawing;
using TagCloud.Core.Interfaces;
using TagCloud.Core.Primitives;

namespace TagCloud.Core.Domain
{
    public class StyleProvider : IStyleProvider
    {
        private readonly IWordsProvider wordsProvider;
        private readonly IStyleSettingsProvider settingsProvider;

        private const float BaseFontSize = 10;

        public StyleProvider(IWordsProvider wordsProvider, IStyleSettingsProvider settingsProvider)
        {
            this.wordsProvider = wordsProvider;
            this.settingsProvider = settingsProvider;
        }

        public Color Background => settingsProvider.StyleSettings.BackgroundColor;

        public Style GetStyle(string word)
        {
            var fontSize = BaseFontSize*wordsProvider.GetFrequency(word);
            var font = new Font(settingsProvider.StyleSettings.FontFamily, fontSize);
            return new Style(font, settingsProvider.StyleSettings.FontColor);
        }
    }
}
