using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Inerfaces;
using McMaster.Extensions.CommandLineUtils;

namespace CLI.Commands
{
    [Command("setCon", Description = "Set WinCC connection string")]
    class SetConnectionCommand : CommandBase
    {
        [Argument(0)]
        [Required]
        public string ConnectionName { get; set; }

        [Argument(1)]
        [Required]
        public string Parameter { get; set; }

        public async Task OnExecute()
        {
            await LoadDatabase();
            await SetConnectionParameter();
            CloseDatabase();
        }

        private async Task SetConnectionParameter()
        {
            var connection = ConnectionService.Connections.FirstOrDefault(c => c.ConnectionName.Equals(ConnectionName, StringComparison.InvariantCultureIgnoreCase));
            if (connection == null)
            {
                Console.WriteLine($"No connection with name '{ConnectionName}' available.");
                return;
            }

            connection.Parameter = Parameter;
            await ConnectionService.UpdateConectionParameter(connection);
            return;
        }

        public SetConnectionCommand(IConnectionService connectionService, IDatabaseService databaseService) : base(connectionService, databaseService)
        {
        }
    }
}
