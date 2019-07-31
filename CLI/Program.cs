using System;
using System.Linq;
using System.Threading.Tasks;
using CLI.Inerfaces;
using CLI.Logic;
using CLI.Model;
using ConsoleTables;
using Microsoft.EntityFrameworkCore;

namespace CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length < 1)
            {
                ShowUsage();
                return;
            }

            var path = args[0];
            var cmd = args[1];

            IConnectionService connectionService = new ConnectionService();
            await connectionService.LoadDatabase(@".\WINCC", path);

            switch (cmd)
            {
                case "list":
                {
                    ConsoleTable
                        .From(connectionService.Connections)
                        .Write(Format.Alternative);

                    break;
                }

                case "set":
                {
                    if (args.Length < 4)
                    {
                        ShowUsage();
                        return;
                    }
                
                    var connectionName = args[2];
                    var parameter = args[3];
                    await SetConnectionParameter(connectionService, connectionName, parameter);
                    break;
                }
            }

            connectionService.CloseDatabase();
        }

        private static async Task SetConnectionParameter(IConnectionService connectionService, string connectionName, string parameter)
        {
            var connection = connectionService.Connections.FirstOrDefault(c => c.ConnectionName.Equals(connectionName, StringComparison.InvariantCultureIgnoreCase));
            if (connection == null)
            {
                Console.WriteLine($"No connection with name '{connectionName}' available.");
                return;
            }

            connection.Parameter = parameter;
            await connectionService.UpdateConectionParameter(connection);
            return;
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Usage: <path to db> <cmd>");
            Console.WriteLine("Commands: list");
            Console.WriteLine("Commands: set <connectionName> <newParameter>");
        }
    }
}