using System.Drawing;
using TagCloud.Core.Infratructure;
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

        public Result<Style> GetStyle(string word)
        {
            return wordsProvider.GetFrequency(word)
                .Then(frequency => new Font(settingsProvider.StyleSettings.FontFamily, BaseFontSize*frequency))
                .Then(font => new Style(font, settingsProvider.StyleSettings.FontColor));
        }
    }
}
