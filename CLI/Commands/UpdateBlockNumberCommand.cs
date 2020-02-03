using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Inerfaces;
using CLI.Logic;
using CLI.Model;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.EntityFrameworkCore;

namespace CLI.Commands
{
    [Command("setOpcNS", Description = "Update OPC Namespace number of all variables in a connection")]
    class UpdateBlockNumberCommand : CommandBase
    {
        [Argument(0)]
        [Required]
        public string ConnectionName { get; set; }

        [Argument(1)]
        [Required]
        public int NewBlockNumber { get; set; }

        public async Task OnExecute()
        {
            await LoadDatabase();
            await variableService.LoadVariables();
            await UpdateBlockname();
            CloseDatabase();
        }

        public async Task UpdateBlockname()
        {
            var connection = ConnectionService.Connections.FirstOrDefault(c => c.ConnectionName.Equals(ConnectionName, StringComparison.InvariantCultureIgnoreCase));
            if (connection == null)
            {
                Console.WriteLine($"No connection with name '{ConnectionName}' available.");
                return;
            }

            var connectionId = connection.ConnectionId;
            var varsToUpdate = variableService.Variables.Where(variable => variable.ConnectionId == connectionId);

            using (var dbcxtransaction = DatabaseService.BeginTransaction())
            {
                try
                {
                    //databaseService.CurrentContext.AddRange(varsToUpdate);
                    foreach (var variable in varsToUpdate)
                    {
                        variable.Blockname = NewBlockNumber;
                        DatabaseService.CurrentContext.Entry(variable).State = EntityState.Modified;
                        //await variableService.UpdateVariable(variable);
                    }

                    dbcxtransaction.Commit();
                    await DatabaseService.CurrentContext.SaveChangesAsync();
                    Console.WriteLine($"Block number successfully updated in {varsToUpdate.Count()} rows");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    Console.WriteLine($"Rollback");
                    dbcxtransaction.Rollback();
                }
            }
        }

        public UpdateBlockNumberCommand(IConnectionService connectionService, IVariableService variableService, IDatabaseService databaseService) : base(connectionService, databaseService)
        {
            this.variableService = variableService;
        }

        private readonly IVariableService variableService;
    }
}
