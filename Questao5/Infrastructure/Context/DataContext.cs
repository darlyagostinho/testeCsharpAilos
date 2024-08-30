using Microsoft.EntityFrameworkCore;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContaCorrente>().HasKey(m => m.NumeroConta);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ContaCorrente> ContaCorrente { get; set; }
    }
}
