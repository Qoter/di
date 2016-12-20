using System;
using System.Drawing;
using System.Linq;
using Autofac;
using CommandLine;
using TagCloud.Core.Domain;
using TagCloud.Core.Interfaces;

namespace TagCloud.Client.ConsoleClient
{
    public class ConsoleUi : CloudUiBase
    {
        private readonly ParserResult<ConsoleUiArgs> argsParseResult;


        public ConsoleUi(string[] args)
        {
            argsParseResult = Parser.Default.ParseArguments<ConsoleUiArgs>(args);
        }

        public override void Run()
        {
            if (argsParseResult.Errors.Any())
            {
                foreach (var error in argsParseResult.Errors)
                {
                    Console.WriteLine(error);
                }
                return;
            }

            var container = BuildContainer();
            container.Resolve<ICloudSaver>().Save();
        }


        protected override AppSettings GetSettings()
        {
            var arguments = argsParseResult.Value;

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
