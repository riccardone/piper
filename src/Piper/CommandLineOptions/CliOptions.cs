using System;
using CommandLine;

namespace Piper.CommandLineOptions
{
    public class CliOptions : IOptions
    {
        [Option('r', "read", Required = true,
            HelpText = "Input files to be processed.")]
        public string Config { get; set; }
    }
}
