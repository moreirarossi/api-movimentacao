using Microsoft.EntityFrameworkCore;
using Movimentacao.Domain.Entities;
using Movimentacao.Domain.Model;
using Movimentacao.Infrastructure.Data.Configurations;

namespace Movimentacao.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<ProdutoCosif> ProdutoCosif { get; set; }
        public DbSet<MovimentoManual> MovimentoManual { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoCosifConfiguration());
            modelBuilder.ApplyConfiguration(new MovimentoManualConfiguration());
            modelBuilder.Entity<MovimentoManualSPModel>().HasNoKey().ToView(null);

        }
    }
}