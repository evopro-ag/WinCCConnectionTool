using System.Collections.Generic;
using System.Threading.Tasks;
using CLI.Model;
using Microsoft.EntityFrameworkCore.Storage;

namespace CLI.Inerfaces
{
    public interface IConnectionService
    {
        Task LoadConnections();
        IEnumerable<Connection> Connections { get; }
        Task UpdateConectionParameter(Connection connection);
    }
}