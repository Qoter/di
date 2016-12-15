using System.Drawing;
using System.Drawing.Imaging;
using Autofac;
using TagCloud.Core;
using TagCloud.Core.Interfaces;

namespace TagCloud.Console
{
    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            var txtWordsFilePath = args.Length > 0 ? args[0] : "default.txt";

            var builder = new ContainerBuilder();

            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<FrequencyStyleProvider>().As<IStyleProvider>().SingleInstance();
            builder.RegisterType<LowerLongPreprocessor>().As<IWordsPreprocessor>();

            builder
                .RegisterType<WordsProvider>()
                .As<IWordsProvider>()
                .WithParameter(new TypedParameter(typeof(string), txtWordsFilePath))
                .SingleInstance();

            builder.RegisterType<CloudRenderer>().AsSelf();

            var container = builder.Build();

            var renderer = container.Resolve<CloudRenderer>();

            renderer.Render(new Size(600, 800)).Save("out.png", ImageFormat.Png);
        }
    }
}
