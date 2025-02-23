using FluentAssertions;
using FluxoCaixa.Consolidado.Modules.Context;
using FluxoCaixa.Consolidado.Modules.Repositories;
using Microsoft.EntityFrameworkCore;

public class ConsolidadoRepositoryTests
{
    private readonly DbContextOptions<AppDbContext> _dbContextOptions;

    public ConsolidadoRepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task AtualizarConsolidadoAsync_DeveCriarNovoConsolidado_SeNaoExistir()
    {
        await using var context = new AppDbContext(_dbContextOptions);
        var repository = new ConsolidadoRepository(context);
        var data = DateTime.UtcNow;
        var valor = 100m;

        await repository.AtualizarConsolidadoAsync(data, valor);
        var consolidado = await repository.ObterConsolidadoPorDataAsync(data);

        consolidado.Should().NotBeNull();
        consolidado!.Data.Should().Be(data.Date);
        consolidado.Saldo.Should().Be(valor);
    }

    [Fact]
    public async Task AtualizarConsolidadoAsync_DeveAtualizarSaldo_SeConsolidadoExistir()
    {
        await using var context = new AppDbContext(_dbContextOptions);
        var repository = new ConsolidadoRepository(context);
        var data = DateTime.UtcNow;
        var valorInicial = 100m;
        var valorAdicional = 50m;

        await repository.AtualizarConsolidadoAsync(data, valorInicial);

        await repository.AtualizarConsolidadoAsync(data, valorAdicional);
        var consolidado = await repository.ObterConsolidadoPorDataAsync(data);

        consolidado.Should().NotBeNull();
        consolidado!.Saldo.Should().Be(valorInicial + valorAdicional);
    }

    [Fact]
    public async Task ObterConsolidadoPorDataAsync_DeveRetornarConsolidado_SeExistir()
    {
        await using var context = new AppDbContext(_dbContextOptions);
        var repository = new ConsolidadoRepository(context);
        var data = DateTime.UtcNow;
        var valor = 150m;

        await repository.AtualizarConsolidadoAsync(data, valor);

        var consolidado = await repository.ObterConsolidadoPorDataAsync(data);

        consolidado.Should().NotBeNull();
        consolidado!.Saldo.Should().Be(valor);
    }

    [Fact]
    public async Task ObterConsolidadoPorDataAsync_DeveRetornarNull_SeNaoExistir()
    {
        await using var context = new AppDbContext(_dbContextOptions);
        var repository = new ConsolidadoRepository(context);
        var data = DateTime.UtcNow;

        var consolidado = await repository.ObterConsolidadoPorDataAsync(data);

        consolidado.Should().BeNull();
    }
}
