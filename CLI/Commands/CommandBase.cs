using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CLI.Exceptions;
using CLI.Inerfaces;
using McMaster.Extensions.CommandLineUtils;

namespace CLI.Commands
{
    public class CommandBase
    {
        protected readonly IDatabaseService DatabaseService;

        [Option("--path", Description = "Path of MDF file")]
        public string Path { get; set; }
        
        [Option("--dbName", Description = "Name of MDF file")]
        public string DbName { get; set; }


        protected readonly IConnectionService ConnectionService;

        public CommandBase(IConnectionService connectionService, IDatabaseService databaseService)
        {
            this.DatabaseService = databaseService;
            this.ConnectionService = connectionService;
        }

        protected async Task LoadDatabase()
        {
            if (string.IsNullOrEmpty(Path))
            {
                if (string.IsNullOrEmpty(DbName))
                {
                    SearchMdfFile();
                }
                else
                {
                    SearchMdfFile(DbName);
                }
            }

            await LoadDatabase(Path);
        }
        
        private async Task LoadDatabase(string path)
        {
            await DatabaseService.LoadDatabase(@".\WINCC", path);
            await ConnectionService.LoadConnections();
        }

        protected void CloseDatabase()
        {
            DatabaseService.CloseDatabase();
        }

        protected void SearchMdfFile(string pattern = null)
        {
            var listOfFiles = Directory.EnumerateFiles(Environment.CurrentDirectory, pattern ?? "*.MDF",
                SearchOption.AllDirectories);
            if (listOfFiles.Count() == 1)
            {
                Path = listOfFiles.First();
            }
            else if (listOfFiles.Count() > 1)
            {
                throw new InvalidCommandException(
                    "Found more than 1 MDF file. Please specify which one to use via the --path or --dbName option");
            }
            else
            {
                throw new InvalidCommandException(
                    "Could not find MDF file. Please specify which one to use via the --path or --dbName option");
            }
        }
    }
}
