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
        public string Path { get; set; }

        [Argument(1)]
        [Required]
        public string ConnectionName { get; set; }

        [Argument(2)]
        [Required]
        public string Parameter { get; set; }

        public async Task OnExecute()
        {
            await LoadDatabase(Path);
            await SetConnectionParameter();
            Close();
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

        public SetCommand(IConnectionService connectionService) : base(connectionService)
        {
        }
    }
}
