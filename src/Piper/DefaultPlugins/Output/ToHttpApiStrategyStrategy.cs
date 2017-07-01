using System;
using Piper.PluginModel;

namespace Piper.DefaultPlugins.Output
{
    public class ToHttpApiStrategyStrategy : IServiceStrategyFactory
    {
        public string StrategyName => typeof(ToHttpApiStrategyStrategy).Name;
        public IServiceStrategy Create()
        {
            return new ToHttpApiStrategy();
        }
    }
}
