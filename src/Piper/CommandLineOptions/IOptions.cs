using CommandLine;

namespace Piper.CommandLineOptions
{
    public interface IOptions
    {
        [Option(HelpText = "The name of the config file", Required = false)]
        string Config { get; set; }
    }
}
