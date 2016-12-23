using System;
using Autofac;
using TagCloud.Core.Domain;
using TagCloud.Core.Infratructure;
using TagCloud.Core.Interfaces;
using TagCloud.Core.Settings;

namespace TagCloud.Client
{
    public abstract class CloudUiBase
    {
        public abstract void Run();

        protected abstract Result<AppSettings> GetSettings();

        protected virtual void SetupContainer(ContainerBuilder builder)
        {
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<StyleProvider>().As<IStyleProvider>().SingleInstance();
            builder.RegisterType<LowerLongPreprocessor>().As<IWordsPreprocessor>();
            builder.RegisterType<WordsProvider>().As<IWordsProvider>();
            builder.RegisterType<CloudRenderer>().As<ICloudRenderer>();
            builder.RegisterType<CloudSaver>().As<ICloudSaver>();
        }

        private void RegisterSettings(ContainerBuilder builder, Func<AppSettings> getSettings=null)
        {
            builder.Register(c => getSettings?.Invoke() ?? GetSettings().GetValueOrThrow())
                  .As<AppSettings>()
                  .As<IInputSettingsProvider>()
                  .As<ICloudSettingsProvider>()
                  .As<IStyleSettingsProvider>()
                  .As<IOutputSettingsProvider>();
        }

        protected IContainer BuildContainer(Func<AppSettings> buildContainer=null)
        {
            var builder = new ContainerBuilder();
            SetupContainer(builder);
            RegisterSettings(builder, buildContainer);
            return builder.Build();
        }
    }
}