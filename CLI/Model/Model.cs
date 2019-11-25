using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CLI.Model
{
    public class Context : DbContext
    {
        public DbSet<Connection> Connection { get; set; }
        public DbSet<Variable> Variables { get; set; }

        public Context() { }
        public Context(DbContextOptions<Context> options)
            : base(options) { }
    }

}
