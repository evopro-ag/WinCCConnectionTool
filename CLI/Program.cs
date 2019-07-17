using System;
using System.Linq;
using System.Threading.Tasks;
using CLI.Inerfaces;
using CLI.Logic;
using CLI.Model;
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
                    var connectionNames = string.Join(Environment.NewLine, connectionService.Connections);
                    Console.WriteLine(connectionNames);
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
                    var connection = connectionService.Connections.FirstOrDefault(c => c.ConnectionName == connectionName);
                    connection.Parameter = parameter;
                    await connectionService.UpdateConectionParameter(connection);
                    break;
                }
            }

            connectionService.CloseDatabase();
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Usage: <path to db> <cmd>");
            Console.WriteLine("Commands: list");
            Console.WriteLine("Commands: set <connectionName> <newParameter>");
        }
    }
}