using System;
using System.Drawing;
using Autofac;
using Fclp;
using TagCloud.Core.Domain;
using TagCloud.Core.Interfaces;

namespace TagCloud.Client.ConsoleClient
{
    public class ConsoleUi : CloudUiBase
    {
        private readonly string[] args;

        public ConsoleUi(string[] args)
        {
            this.args = args;
        }

        public override void Run()
        {
            var container = BuildContainer();

            container.Resolve<ICloudSaver>().Save();
        }

        private static FluentCommandLineParser<ConsoleUiArgs> SetupParser()
        {
            var p = new FluentCommandLineParser<ConsoleUiArgs>();

            p.Setup(arg => arg.SourcePath)
             .As('s', "source")
             .SetDefault("defaul.txt")
             .WithDescription("Path to source file with word");

            p.Setup(arg => arg.Width)
             .As('w', "width")
             .SetDefault(1024)
             .WithDescription("Result image width");

            p.Setup(arg => arg.Height)
             .As('h', "height")
             .SetDefault(1024)
             .WithDescription("Result image height");

            p.Setup(arg => arg.BackgroundColor)
            .As('b', "background")
            .SetDefault("#ffffff")
            .WithDescription("Background color format #AAGGBB");

            p.Setup(arg => arg.FontColor)
             .As('c', "color")
             .SetDefault("#000000")
             .WithDescription("Font color format #AAGGBB");

            p.Setup(arg => arg.Font)
             .As("font")
             .SetDefault("Arial")
             .WithDescription("Font name");

            p.Setup(arg => arg.SpiralStep)
                .As("spiral-step")
                .SetDefault(1)
                .WithDescription("Greater step - greater spiral");

            p.Setup(arg => arg.ImageFormat)
                .As('f', "format")
                .SetDefault("png")
                .WithDescription("Format of image: (png, jpg, bmp)");

            p.Setup(arg => arg.OutputFilename)
                .As('o', "output")
                .SetDefault("out")
                .WithDescription("Output filename");

            p.SetupHelp("?", "helo")
             .Callback(text => Console.WriteLine(text));

            return p;
        }

        protected override AppSettings GetSettings()
        {
            var parser = SetupParser();
            parser.Parse(args);

            var arguments = parser.Object;

            return new AppSettings()
            {
                CloudSettings = new CloudSettings()
                {
                    Size = new Size(arguments.Width, arguments.Height),
                    SpiralFactor = Math.Max(1, arguments.SpiralStep) / Math.PI
                },

                OutputSettings = new OutputSettings()
                {
                    ImageFormat = arguments.ImageFormat,
                    OutputFilename = arguments.OutputFilename
                },

                StyleSettings = new StyleSettings()
                {
                    BackgroundColor = ColorTranslator.FromHtml(arguments.BackgroundColor),
                    FontColor = ColorTranslator.FromHtml(arguments.FontColor),
                    FontFamily = new FontFamily(arguments.Font)
                },

                WordsDirectory = arguments.SourcePath
            };
        }
    }
}
