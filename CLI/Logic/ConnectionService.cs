using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CLI.Inerfaces;
using CLI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CLI.Logic
{
    public class ConnectionService : IConnectionService
    {
        private readonly IDatabaseService databaseService;

        public ConnectionService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }
        public async Task LoadConnections()
        {
            Connections = await databaseService.CurrentContext.Connection.ToArrayAsync();
        }

        public async Task UpdateConectionParameter(Connection connection)
        {
            databaseService.CurrentContext.Update(connection);
            await databaseService.CurrentContext.SaveChangesAsync();
        }

        public IEnumerable<Connection> Connections { get; private set;  }
    }
}