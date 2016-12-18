using Autofac;
using TagCloud.Core.Domain;
using TagCloud.Core.Interfaces;

namespace TagCloud.Client
{
    public abstract class CloudUiBase
    {
        public abstract void Run();

        protected abstract AppSettings GetSettings();

        protected virtual ContainerBuilder SetupContainer(ContainerBuilder builder)
        {
            builder.Register(c => GetSettings())
                .As<AppSettings>()
                .As<IWordsDirectoryProvider>()
                .As<ICloudSettingsProvider>()
                .As<IStyleSettingsProvider>()
                .As<IOutputSettingsProvider>();

            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<StyleProvider>().As<IStyleProvider>().SingleInstance();
            builder.RegisterType<LowerLongPreprocessor>().As<IWordsPreprocessor>();
            builder.RegisterType<WordsProvider>().As<IWordsProvider>();
            builder.RegisterType<CloudRenderer>().As<ICloudRenderer>();
            builder.RegisterType<CloudSaver>().As<ICloudSaver>();

            return builder;
        }

        protected IContainer BuildContainer()
        {
            return SetupContainer(new ContainerBuilder()).Build();
        }
    }
}