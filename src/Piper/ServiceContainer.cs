using log4net;
using Piper.PluginModel;

namespace Piper
{
    internal class ServiceContainer : IServiceStrategy
    {
        private readonly IServiceStrategyFactory[] _factories;
        private static readonly ILog Log = LogManager.GetLogger(typeof(ServiceContainer));

        public ServiceContainer(IServiceStrategyFactory[] factories)
        {
            _factories = factories;
        }

        public void Stop()
        {
            Log.Info("ServiceContainer stopped");
        }

        public bool Start()
        {
            foreach (var factory in _factories)
            {
                factory.Create().Start();
                Log.Info($"{factory.StrategyName} started");
            }
            Log.Info("All available services started");
            return true;
        }
    }
}
