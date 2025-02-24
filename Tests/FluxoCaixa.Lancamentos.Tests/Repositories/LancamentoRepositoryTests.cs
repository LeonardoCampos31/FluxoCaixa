
using Microsoft.EntityFrameworkCore;
using FluxoCaixa.Lancamentos.Modules.Entity;
using FluxoCaixa.Lancamentos.Modules.Repositories;
using FluxoCaixa.Lancamentos.Modules.DataTransfers.Context;

namespace FluxoCaixa.Lancamentos.Tests.Repositories
{
    public class LancamentoRepositoryTests
    {
        private readonly LancamentoRepository _repository;
        private readonly AppDbContext _context;

        public LancamentoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb") // Banco em memória para testes
                .Options;

            _context = new AppDbContext(options);
            _repository = new LancamentoRepository(_context);
        }

        [Fact]
        public async Task AdicionarLancamentoAsync_DeveAdicionarLancamentoAoContexto()
        {
            var lancamento = new Lancamento 
            { 
                Data = DateTime.UtcNow, 
                Valor = 100, 
                Tipo = "Receita"  // <-- Certifique-se de preencher essa propriedade obrigatória
            };

            await _repository.AdicionarLancamentoAsync(lancamento);

            var lancamentoNoBanco = await _context.Lancamento.FindAsync(lancamento.Id);
            Assert.NotNull(lancamentoNoBanco);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task ObterLancamentosPorDataAsync_DeveRetornarLancamentosDoDia()
        {
            var data = DateTime.UtcNow.Date;
            var lancamento = new Lancamento 
            { 
                Data = data, 
                Valor = 200, 
                Tipo = "Despesa" // <-- Propriedade obrigatória preenchida
            };

            _context.Lancamento.Add(lancamento);
            await _context.SaveChangesAsync();

            var resultado = await _repository.ObterLancamentosPorDataAsync(data);

            Assert.Single(resultado);
            Assert.Contains(resultado, l => l.Valor == 200);
            _context.Database.EnsureDeleted();
        }
    }
}