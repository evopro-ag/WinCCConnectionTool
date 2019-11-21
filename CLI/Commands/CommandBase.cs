using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Inerfaces;
using McMaster.Extensions.CommandLineUtils;

namespace CLI.Commands
{
    public class CommandBase
    {

        [Option("--path", Description = "Path to MDF file")]
        public string Path { get; set; }


        protected IConnectionService ConnectionService { get; set; }

        public CommandBase(IConnectionService connectionService)
        {
            this.ConnectionService = connectionService;
        }

        protected async Task LoadDatabase(string path)
        {
            await ConnectionService.LoadDatabase(@".\WINCC", path);
        }

        protected void Close()
        {
            ConnectionService.CloseDatabase();
        }

        protected void SearchMdfFile()
        {
            if (Path == null)
            {
                var listOfFiles = Directory.EnumerateFiles(Environment.CurrentDirectory, "*.MDF", SearchOption.AllDirectories);
                if (listOfFiles.Count() == 1)
                {
                    Path = listOfFiles.First();
                }else if(listOfFiles.Count() > 1)
                {
                    throw new InvalidCommandException("Found more than 1 MDF file. Please specify which one to use via the --path option");
                }
                else
                {
                    throw new InvalidCommandException("Could not find MDF file. Please specify which one to use via the --path option");
                }
            }
        }
    }

    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(string message) :base(message)
        {

        }
    }
}
