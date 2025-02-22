
using FluxoCaixa.Consolidado.Modules.Entity;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Consolidado.Modules.Context
{
    public class AppDbContext : DbContext
    {
        private IConfiguration _configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options) 
        {
            _configuration = configuration;
        }

        public DbSet<ConsolidadoDiario> ConsolidadoDiario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<ConsolidadoDiario>()
            .ToTable("consolidado") // Nome da tabela no banco
            .HasKey(c => c.Id); // Define a chave primária

        modelBuilder.Entity<ConsolidadoDiario>()
            .Property(c => c.Id)
            .HasColumnName("id"); // Nome da coluna no banco

        modelBuilder.Entity<ConsolidadoDiario>()
            .Property(c => c.Data)
            .HasColumnName("data")
            .HasColumnType("DATE") // Define o tipo de dado no banco
            .IsRequired();

        modelBuilder.Entity<ConsolidadoDiario>()
            .Property(c => c.Saldo)
            .HasColumnName("saldo")
            .HasColumnType("DECIMAL(18,2)") // Define o tipo de dado no banco
            .IsRequired();
        }
    }   
}