using System.Collections.Generic;
using System.Threading.Tasks;
using CLI.Model;

namespace CLI.Inerfaces
{
    internal interface IConnectionService
    {
        Task LoadDatabase(string dbInstance, string dbPath);
        IEnumerable<Connection> Connections { get; }
        void CloseDatabase();
        Task UpdateConectionParameter(Connection connection);
    }
}