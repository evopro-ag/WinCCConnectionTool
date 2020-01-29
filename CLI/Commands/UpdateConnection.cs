using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CLI.Inerfaces;
using McMaster.Extensions.CommandLineUtils;

namespace CLI.Commands
{
    [Command("setOpc", Description = "Update a WinCC connection string of type OPC UA")]
    class UpdateConnection : CommandBase
    {
        [Argument(0)]
        [Required]
        public string ConnectionName { get; set; }

        [Argument(1)]
        [Required]
        public string Host { get; set; }

        [Argument(2)]
        [Required]
        public string Port { get; set; }

        public async Task OnExecute()
        {
            await LoadDatabase();
            await UpdateConnectionString();
            CloseDatabase();
        }

        private async Task UpdateConnectionString()
        {
            var connection = ConnectionService.Connections.FirstOrDefault(c => c.ConnectionName.Equals(ConnectionName, StringComparison.InvariantCultureIgnoreCase));
            if (connection == null)
            {
                Console.WriteLine($"No connection with name '{ConnectionName}' available.");
                return;
            }

            connection.Parameter = ReplaceHostAndPort(connection.Parameter,Host,Port);
            await ConnectionService.UpdateConectionParameter(connection);
            return;
        }


        private string ReplaceHostAndPort(string connectionParam, string host, string port)
        {
            return Regex.Replace(connectionParam, @"^opc.tcp://([a-zA-Z0-9\-._~%]+:[0-9]+)?", delegate (Match match)
            {
                return $"opc.tcp://{host}:{port}";
            });
        }


        public UpdateConnection(IConnectionService connectionService, IDatabaseService databaseService) : base(connectionService, databaseService)
        {
        }
    }
}
