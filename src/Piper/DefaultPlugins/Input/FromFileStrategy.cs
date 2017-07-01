using System;
using Piper.PluginModel;

namespace Piper.DefaultPlugins.Input
{
    public class FromFileStrategy : IServiceStrategy
    {
        public bool Start()
        {
            Console.WriteLine("Do FromFileStrategy something on a separate thread...");
            return true;
        }
    }
}
