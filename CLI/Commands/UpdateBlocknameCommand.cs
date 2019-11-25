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
    [Command("UpdateBlockname", Description = "update Blockname of Variable description")]
    class UpdateBlocknameCommand : CommandBase
    {
        [Argument(0)]
        [Required]
        public string ConnectionName { get; set; }

        [Argument(1)]
        [Required]
        public int NewBlockname { get; set; }

        public async Task OnExecute()
        {
            SearchMdfFile();
            await LoadDatabase(Path);
            await variableService.LoadVariables();
            await UpdateBlockname();
            Close();
        }

        public async Task UpdateBlockname()
        {
            var connection = connectionService.Connections.FirstOrDefault(c => c.ConnectionName.Equals(ConnectionName, StringComparison.InvariantCultureIgnoreCase));
            if (connection == null)
            {
                Console.WriteLine($"No connection with name '{ConnectionName}' available.");
                return;
            }

            var connectionId = connection.ConnectionId;
            var varsToUpdate = variableService.Variables.Where(variable => variable.ConnectionId == connectionId);

            using (var dbcxtransaction = databaseService.BeginTransaction())
            {
                try
                {
                    //databaseService.CurrentContext.AddRange(varsToUpdate);
                    foreach (var variable in varsToUpdate)
                    {
                        variable.Blockname = NewBlockname;
                        databaseService.CurrentContext.Entry(variable).State = EntityState.Modified;
                        //await variableService.UpdateVariable(variable);
                    }

                    dbcxtransaction.Commit();
                    await databaseService.CurrentContext.SaveChangesAsync();
                    Console.WriteLine($"Blockname successfully updated on {varsToUpdate.Count()} rows");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    Console.WriteLine($"Rollback");
                    dbcxtransaction.Rollback();
                }
            }
        }

        public UpdateBlocknameCommand(IConnectionService connectionService, IVariableService variableService, IDatabaseService databaseService) : base(connectionService, databaseService)
        {
            this.variableService = variableService;
        }

        private readonly IVariableService variableService;
    }
}
