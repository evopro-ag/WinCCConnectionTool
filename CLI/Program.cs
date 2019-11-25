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
    [Subcommand(typeof(ListCommand), typeof(SetCommand), typeof(UpdateConnection), typeof(UpdateBlocknameCommand))]
    public class Program
    {
        [Option("--path", Description = "Path to MDF file")]
        public string Path { get; set; }


        private static CommandLineApplication<Program> app;

        public static int Main(string[] args)
        {
            try
            {
                var databaseService = new DatabaseService();
                var services = new ServiceCollection()
                    .AddSingleton<IDatabaseService>(databaseService)
                    .AddSingleton<IConnectionService>(new ConnectionService(databaseService))
                    .AddSingleton<IVariableService>(new VariableService(databaseService))
                    .BuildServiceProvider();

                using (app = new CommandLineApplication<Program>())
                {
                    app.Conventions
                        .UseDefaultConventions()
                        .UseConstructorInjection(services);
                    return app.Execute(args);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                if(ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
                app.ShowHelp();
                return -1;
            }
        }


        public void OnExecute()
        {
            app.ShowHelp();
        }
    }
}