using System;
using Piper.PluginModel;

namespace Piper.DefaultPlugins.Input
{
    public class LogFileInputStrategyFactory : IServiceStrategyFactory
    {
        public string StrategyName => typeof(LogFileInputStrategyFactory).Name;
        public IServiceStrategy Create()
        {
            return new LogFileInputStrategy();
        }
    }
}
