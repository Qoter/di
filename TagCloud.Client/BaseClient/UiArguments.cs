using System.Drawing;
using CommandLine;
using TagCloud.Core.Infratructure;
using TagCloud.Core.Settings;

namespace TagCloud.Client.BaseClient
{
    public class UiArguments
    {
        [Option('s', "source", DefaultValue = "default.txt", HelpText = "Path to file with words by line")]
        public string SourcePath { get; set; }

        [Option('o', "output", DefaultValue = "cloud", HelpText = "Path to output file")]
        public string OutputFilename { get; set; }

        [Option('w', "width", DefaultValue = 1024, HelpText = "Width of result image")]
        public int Width { get; set; }

        [Option('h', "height", DefaultValue = 1024, HelpText = "Height of result image")]
        public int Height { get; set; }

        [Option("bg", DefaultValue = "#ffffff", HelpText = "Background color of result image")]
        public string BackgroundColor { get; set; }

        [Option("font-color", DefaultValue = "#000000", HelpText = "Color of words")]
        public string FontColor { get; set; }

        [Option("font", DefaultValue = "Arial", HelpText = "Font family of words")]
        public string Font { get; set; }

        [Option("step", DefaultValue = 1, HelpText = "k=step/Pi where k is a factor of cloud spiral")]
        public int SpiralStep { get; set; }

        [Option("format", DefaultValue = "png", HelpText = "Format of result image")]
        public string ImageFormat { get; set; }

        public Result<AppSettings> AsAppSettings()
        {
            var appSettings = new AppSettings();
            return InputSettings.Create(SourcePath)
                .Then(inputSettings => appSettings.InputSettings = inputSettings)
                .Then(_ => OutputSettings.FromStrings(ImageFormat, OutputFilename))
                .Then(outputSettings => appSettings.OutputSettings = outputSettings)
                .Then(_ => CloudSettings.Create(new Size(Width, Height), SpiralStep))
                .Then(cloudSettting => appSettings.CloudSettings = cloudSettting)
                .Then(_ => StyleSettings.FromStrings(BackgroundColor, FontColor, Font))
                .Then(styleSetting => appSettings.StyleSettings = styleSetting)
                .Then(_ => appSettings);
        }
    }
}