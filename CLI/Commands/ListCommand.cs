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
    [Command("list", Description = "List all WinCC connections")]
    class ListCommand : CommandBase
    {
        public async Task OnExecute()
        {
            await LoadDatabase();
            
            ConsoleTable
                .From(ConnectionService.Connections)
                .Write(Format.Alternative);
            
            CloseDatabase();
            
        }

        public ListCommand(IConnectionService connectionService, IDatabaseService databaseService) : base(connectionService, databaseService)
        {
        }
    }
}
