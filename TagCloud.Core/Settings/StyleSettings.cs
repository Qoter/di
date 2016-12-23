using System.Drawing;
using System.Text.RegularExpressions;
using TagCloud.Core.Infratructure;

namespace TagCloud.Core.Settings
{
    public class StyleSettings
    {
        public Color BackgroundColor { get; }
        public Color FontColor { get; }
        public FontFamily FontFamily { get; }

        private static readonly Regex ColorPattern = new Regex(@"#[0-9a-f]{6}", RegexOptions.Compiled);

        public StyleSettings(Color backgroundColor, Color fontColor, FontFamily fontFamily)
        {
            BackgroundColor = backgroundColor;
            FontColor = fontColor;
            FontFamily = fontFamily;
        }

        public static Result<StyleSettings> FromStrings(string htmlBgColor, string htmlFontColor, string fontFamily)
        {
            if (!ColorPattern.IsMatch(htmlBgColor))
            {
                return Result.Fail<StyleSettings>($"Backgroud color has incorrect format: {htmlBgColor}");
            }
            if (!ColorPattern.IsMatch(htmlFontColor))
            {
                return Result.Fail<StyleSettings>($"Font color has incorrect format: {htmlFontColor}");
            }

            var bgColor = ColorTranslator.FromHtml(htmlBgColor);
            var fontColor = ColorTranslator.FromHtml(htmlFontColor);
            var font = new FontFamily(fontFamily);

            return Result.Ok(new StyleSettings(bgColor, fontColor, font));
        }
    }
}
