using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;

namespace CLI.Inerfaces
{
    public interface IVariableService
    {
        Task LoadVariables();
        IEnumerable<Variable> Variables { get; }
        Task UpdateVariable(Variable variable);
    }
}
