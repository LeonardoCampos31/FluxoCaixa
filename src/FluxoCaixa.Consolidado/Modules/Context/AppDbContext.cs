
using FluxoCaixa.Consolidado.Modules.Entity;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Consolidado.Modules.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<ConsolidadoDiario> ConsolidadoDiario { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<ConsolidadoDiario>()
            .ToTable("consolidado")
            .HasKey(c => c.Id);

            modelBuilder.Entity<ConsolidadoDiario>()
                .Property(c => c.Id)
                .HasColumnName("id");

            modelBuilder.Entity<ConsolidadoDiario>()
                .Property(c => c.Data)
                .HasColumnName("data")
                .HasColumnType("DATE")
                .IsRequired();

            modelBuilder.Entity<ConsolidadoDiario>()
                .Property(c => c.Saldo)
                .HasColumnName("saldo")
                .HasColumnType("DECIMAL(18,2)")
                .IsRequired();
        }
    }   
}