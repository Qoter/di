using System;
using System.Drawing;
using System.Drawing.Imaging;
using Autofac;
using Fclp;
using TagCloud.Core.Interfaces;
using TagCloud.Core.Model;
using TagCloud.Core.View;


namespace TagCloud.Client.ConsoleClient
{
    public class ConsoleUi : ICloudUi
    {
        private readonly string[] args;

        public ConsoleUi(string[] args)
        {
            this.args = args;
        }

        private static FluentCommandLineParser<ConsoleUiArgs> PrepareParser()
        {
            var p = new FluentCommandLineParser<ConsoleUiArgs>();

            p.Setup(arg => arg.SourcePath)
             .As('s', "source")
             .SetDefault("default.txt")
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
             .As('f', "font")
             .SetDefault("Arial")
             .WithDescription("Font name");

            p.SetupHelp("?", "helo")
             .Callback(text => Console.WriteLine(text));

            return p;
        }

        private static CloudRenderer PrepareRenderer(CloudSettings settings, string sourcePath)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(settings).AsSelf().ExternallyOwned();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<StyleProvider>().As<IStyleProvider>().SingleInstance();
            builder.RegisterType<LowerLongPreprocessor>().As<IWordsPreprocessor>();

            builder
                .RegisterType<WordsProvider>()
                .As<IWordsProvider>()
                .WithParameter(new TypedParameter(typeof(string), sourcePath))
                .SingleInstance();

            builder.RegisterType<CloudRenderer>().AsSelf();


            var container = builder.Build();

            return container.Resolve<CloudRenderer>();
        }

        public void Run()
        {
            var parser = PrepareParser();
            var parseResult = parser.Parse(args);
            if (parseResult.HasErrors)
            {
                Console.WriteLine(parseResult.ErrorText);
                return;
            }

            if (parseResult.HelpCalled)
            {
                return;
            }

            var arguments = parser.Object;

            var settings = new CloudSettings
            {
                Size = new Size(arguments.Width, arguments.Height),
                BackgroundColor = ColorTranslator.FromHtml(arguments.BackgroundColor),
                FontColor = ColorTranslator.FromHtml(arguments.FontColor),
                FontFamily = new FontFamily(arguments.Font)
            };


            var renderer = PrepareRenderer(settings, arguments.SourcePath);

            renderer.Render().Save("out.png", ImageFormat.Png);
        }
    }
}
