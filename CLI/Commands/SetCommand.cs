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
    [Command("set", Description = "set DB parameters")]
    class SetCommand : CommandBase
    {
        [Argument(0)]
        [Required]
        public string ConnectionName { get; set; }

        [Argument(1)]
        [Required]
        public string Parameter { get; set; }

        public async Task OnExecute()
        {
            SearchMdfFile();
            await LoadDatabase(Path);
            await SetConnectionParameter();
            Close();
        }

        private async Task SetConnectionParameter()
        {
            var connection = connectionService.Connections.FirstOrDefault(c => c.ConnectionName.Equals(ConnectionName, StringComparison.InvariantCultureIgnoreCase));
            if (connection == null)
            {
                Console.WriteLine($"No connection with name '{ConnectionName}' available.");
                return;
            }

            connection.Parameter = Parameter;
            await connectionService.UpdateConectionParameter(connection);
            return;
        }

        public SetCommand(IConnectionService connectionService, IDatabaseService databaseService) : base(connectionService, databaseService)
        {
        }
    }
}
