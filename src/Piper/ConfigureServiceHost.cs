using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using CommandLine;
using log4net;
using log4net.Config;
using Piper.CommandLineOptions;
using Piper.PluginModel;
using Topshelf;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace Piper
{
    public static class ConfigureServiceHost
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ConfigureServiceHost));
        public static void Configure(string[] args)
        {
            if (ConfigurationManager.GetSection("log4net") != null)
                XmlConfigurator.Configure();
            else
                Logger.Setup();
            
            var deserialiser = new Deserializer();
            const string configPath = @"piper.yaml";

            try
            {
                var options = new CliOptions();
                var isValid = Parser.Default.ParseArguments(args, options);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            var config = deserialiser.Deserialize<dynamic>(configPath);

            var f = File.ReadAllText(configPath);

            // Setup the input
            var input = new StringReader(configPath);

            // Load the stream
            var yaml = new YamlStream();
            yaml.Load(input);

            var mapping =
                yaml.Documents[0].RootNode;

            var deserializer = new Deserializer();
            var result = deserializer.Deserialize<List<Hashtable>>(File.ReadAllText(configPath));
            foreach (var item in result)
            {
                Console.WriteLine("Item:");
                foreach (DictionaryEntry entry in item)
                {
                    Console.WriteLine("- {0} = {1}", entry.Key, entry.Value);
                }
            }

            var serviceContainersFactories = GetStrategyFactoriesFromPlugins();
            HostFactory.Run(x =>
            {
                x.UseLog4Net();
                x.Service<ServiceContainer>(s =>
                {
                    s.ConstructUsing(name => new ServiceContainer(serviceContainersFactories));
                    s.WhenStarted(
                        (tc, hostControl) =>
                            tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.SetDescription("This process load and run Application Service modules from the plugins directory");
                x.SetDisplayName("Piper Host");
                x.SetServiceName("Piper");
            });
        }

        private static IServiceStrategyFactory[] GetStrategyFactoriesFromPlugins()
        {
            PluginLoader.LoadPlugins("plugins", Log);
            var plugins =
                AppDomain.CurrentDomain.GetAssemblies()
                    //.Where(a => a.FullName.Contains("Plugin") && !a.FullName.Contains("PluginModel"))
                    .ToList();
            return (from domainAssembly in plugins
                    from assemblyType in domainAssembly.GetTypes()
                    where typeof(IServiceStrategyFactory).IsAssignableFrom(assemblyType) && !assemblyType.IsInterface
                    select assemblyType).ToArray().Select(Activator.CreateInstance)
                .Select(instance => instance)
                .Cast<IServiceStrategyFactory>()
                .ToArray();
        }
    }
}
