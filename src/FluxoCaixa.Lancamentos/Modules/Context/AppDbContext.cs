using FluxoCaixa.Lancamentos.Modules.Entity;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Lancamentos.Modules.DataTransfers.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Lancamento> Lancamento { get; set; }

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