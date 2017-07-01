using System;
using Piper.PluginModel;

namespace Piper.DefaultPlugins.Output
{
    public class ToHttpApiStrategy : IServiceStrategy
    {
        public bool Start()
        {
            Console.WriteLine("Do ToHttpApiStrategy something on a separate thread...");
            return true;
        }
    }
}
