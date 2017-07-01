using System;
using Piper.PluginModel;

namespace Piper.DefaultPlugins.Input
{
    public class FromFileStrategyFactory : IServiceStrategyFactory
    {
        public string StrategyName => typeof(FromFileStrategyFactory).Name;
        public IServiceStrategy Create()
        {
            return new FromFileStrategy();
        }
    }
}
