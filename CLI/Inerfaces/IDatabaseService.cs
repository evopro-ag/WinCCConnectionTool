using System;
using System.Linq;
using System.Threading.Tasks;
using CLI.Model;
using Microsoft.EntityFrameworkCore.Storage;

namespace CLI.Inerfaces
{
    public interface IDatabaseService
    {
        Task LoadDatabase(string dbInstance, string dbPath);
        void CloseDatabase();
        IDbContextTransaction BeginTransaction();
        Context CurrentContext { get; }

    }
}