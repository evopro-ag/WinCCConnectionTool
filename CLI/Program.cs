using System;
using System.Linq;
using System.Threading.Tasks;
using CLI.Commands;
using CLI.Inerfaces;
using CLI.Logic;
using CLI.Model;
using ConsoleTables;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CLI
{
    [Subcommand(typeof(ListCommand), typeof(SetCommand))]
    public class Program
    {
        private static CommandLineApplication<Program> app;

        public static int Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSingleton<IConnectionService>(new ConnectionService())
                .BuildServiceProvider();

            using (app = new CommandLineApplication<Program>())
            {
                app.Conventions
                    .UseDefaultConventions()
                    .UseConstructorInjection(services);
                return app.Execute(args);
            }
        }


        public void OnExecute()
        {
            app.ShowHelp();
        }
    }
}