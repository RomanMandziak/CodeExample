using CodeExample.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CodeExample.Core.Db
{
    public class PriceDbContext : DbContext
    {
        public PriceDbContext(DbContextOptions<PriceDbContext> options)
            : base(options) { }

        public DbSet<Market> Markets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(PriceDbContext).Assembly);
        }
    }
}
