using FluxoCaixa.Lancamentos.Modules.Models;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Lancamentos.Modules.DataTransfers.Context
{
    public class AppDbContext : DbContext
    {
        private IConfiguration _configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options) 
        {
            _configuration = configuration;
        }

        public DbSet<Lancamento> Lancamento { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lancamento>()
            .ToTable("lancamento")
            .HasKey(l => l.Id);

        modelBuilder.Entity<Lancamento>()
            .Property(l => l.Id)
            .HasColumnName("id");

        modelBuilder.Entity<Lancamento>()
            .Property(l => l.Valor)
            .HasColumnName("valor")
            .HasColumnType("DECIMAL(18,2)");

        modelBuilder.Entity<Lancamento>()
            .Property(l => l.Tipo)
            .HasColumnName("tipo")
            .HasMaxLength(10);

        modelBuilder.Entity<Lancamento>()
            .Property(l => l.Data)
            .HasColumnName("data")
            .HasColumnType("TIMESTAMP WITH TIME ZONE");
        }
    }   
}