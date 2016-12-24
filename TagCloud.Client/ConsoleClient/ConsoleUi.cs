using System;
using System.Linq;
using Autofac;
using CommandLine;
using TagCloud.Core.Infratructure;
using TagCloud.Core.Interfaces;
using TagCloud.Core.Settings;

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
            GetSettings()
                .Then(SaveCloud)
                .OnFail(Console.WriteLine);
        }

        protected override Result<AppSettings> GetSettings()
        {
            return ParseArguments(args).Then(uiArgs => uiArgs.AsAppSettings());
        }

        private Result<None> SaveCloud(AppSettings settings)
        {
            var container = BuildContainer(() => settings);
            return container.Resolve<ICloudSaver>().Save();
        }

        private static Result<UiArguments> ParseArguments(string[] args)
        {
            var parseResult = Parser.Default.ParseArguments<UiArguments>(args);
            if (parseResult.Errors.Any())
            {
                var errorText = string.Join(Environment.NewLine, parseResult.Errors.Select(e => e.ToString()));
                return Result.Fail<UiArguments>(errorText);
            }

            return Result.Ok(parseResult.Value);
        }
    }
}
