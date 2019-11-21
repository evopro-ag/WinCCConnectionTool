using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Argument(0)]
        [Required]
        public string Path { get; set; }

        public async Task OnExecute()
        {
            await LoadDatabase(Path);
            IConnectionService connectionService = new ConnectionService();
            await connectionService.LoadDatabase(@".\WINCC", Path);

            ConsoleTable
                .From(connectionService.Connections)
                .Write(Format.Alternative);
            Close();
        }

        public ListCommand(IConnectionService connectionService) : base(connectionService)
        {
        }
    }
}
