using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Inerfaces;
using CLI.Model;
using Microsoft.EntityFrameworkCore;

namespace CLI.Logic
{
    public class VariableService : IVariableService
    {
        private readonly IDatabaseService databaseService;

        public VariableService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }
        public async Task LoadVariables()
        {
            Variables = await databaseService.CurrentContext.Variables.ToArrayAsync();
        }

        public async Task UpdateVariable(Variable variable)
        {
            databaseService.CurrentContext.Update(variable);
            databaseService.CurrentContext.Entry(variable).State = EntityState.Modified;
            await databaseService.CurrentContext.SaveChangesAsync(false);
        }


        public IEnumerable<Variable> Variables { get; private set; }
    }
}
