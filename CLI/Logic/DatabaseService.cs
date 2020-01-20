using System.Reactive;
using System.Threading.Tasks;
using CLI.Inerfaces;
using CLI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CLI.Logic
{
    public class DatabaseService : IDatabaseService
    {
        public Task LoadDatabase(string dbInstance, string dbPath)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();

            optionsBuilder.UseSqlServer($@"Data Source={dbInstance};Integrated Security=SSPI;AttachDbFilename={dbPath};TrustServerCertificate=true");


            CurrentContext = new Context(optionsBuilder.Options);

            return Task.FromResult(Unit.Default);
        }

        public void CloseDatabase()
        {
            CurrentContext.Dispose();
        }

        

        public IDbContextTransaction BeginTransaction()
        {
            return CurrentContext.Database.BeginTransaction();
        }

        public Context CurrentContext { get; private set; }
    }
}