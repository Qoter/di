using CommandLine;

namespace TagCloud.Client.ConsoleClient
{
    public class ConsoleUiArgs
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

    }
}