using System.Collections.Generic;
using System.Threading.Tasks;
using CLI.Inerfaces;
using CLI.Model;
using Microsoft.EntityFrameworkCore;

namespace CLI.Logic
{
    internal class ConnectionService : IConnectionService
    {
        private Context context;

        public async Task LoadDatabase(string dbInstance, string dbPath)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();

            optionsBuilder.UseSqlServer($@"Data Source={dbInstance};Integrated Security=SSPI;AttachDbFilename={dbPath};app=LINQPad;TrustServerCertificate=true");


            context = new Context(optionsBuilder.Options);

            Connections = await context.Connection.ToArrayAsync();

        }

        public void CloseDatabase()
        {
            context.Dispose();
        }

        public async Task UpdateConectionParameter(Connection connection)
        {
            context.Update(connection);
            await context.SaveChangesAsync();
        }

        public IEnumerable<Connection> Connections { get; private set;  }
    }
}