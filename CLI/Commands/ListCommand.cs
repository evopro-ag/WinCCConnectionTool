using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Inerfaces;
using CLI.Logic;
using ConsoleTables;
using McMaster.Extensions.CommandLineUtils;

namespace CLI.Commands
{
    [Command("list", Description = "list DB connections")]
    class ListCommand : CommandBase
    {
        public async Task OnExecute()
        {
            SearchMdfFile();
            await LoadDatabase(Path);
            
            ConsoleTable
                .From(connectionService.Connections)
                .Write(Format.Alternative);
            Close();
            
        }

        public ListCommand(IConnectionService connectionService, IDatabaseService databaseService) : base(connectionService, databaseService)
        {
        }
    }
}
