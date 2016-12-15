using Autofac;
using Autofac.Core;
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

            builder.RegisterType<CircularRectangleLayouter>().As<IRectangleLayouter>();
            builder.RegisterType<DefaultStyleProvider>().As<IStyleProvider>();
            builder.RegisterType<LowerLongPreprocessor>().As<IWordsPreprocessor>();
            builder.RegisterType<TagLayouter>().AsSelf();

            builder.RegisterType<WordsProvider>()
                   .As<IWordsProvider>()
                   .WithParameter(new TypedParameter(typeof(string), txtWordsFilePath));

            builder.RegisterType<CloudRenderer>().AsSelf();

            var container = builder.Build();

            var renderer = container.Resolve<CloudRenderer>();
        }
    }
}
