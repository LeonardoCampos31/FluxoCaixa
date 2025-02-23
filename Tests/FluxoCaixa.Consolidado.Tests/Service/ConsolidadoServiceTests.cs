using FluentAssertions;
using FluxoCaixa.Consolidado.Modules.Entity;
using FluxoCaixa.Consolidado.Modules.Repositories;
using FluxoCaixa.Consolidado.Modules.Services;
using Moq;

public class ConsolidadoServiceTests
{
    private readonly Mock<IConsolidadoRepository> _repositoryMock;
    private readonly ConsolidadoService _service;

    public ConsolidadoServiceTests()
    {
        _repositoryMock = new Mock<IConsolidadoRepository>();
        _service = new ConsolidadoService(_repositoryMock.Object);
    }

    [Fact]
    public async Task AtualizarConsolidadoAsync_DeveChamarRepositoryComParametrosCorretos()
    {
        var data = DateTime.UtcNow;
        var valor = 100m;

        await _service.AtualizarConsolidadoAsync(data, valor);

        _repositoryMock.Verify(r => r.AtualizarConsolidadoAsync(data, valor), Times.Once);
    }

    [Fact]
    public async Task ObterConsolidadoPorDataAsync_DeveRetornarConsolidado_SeExistir()
    {
        var data = DateTime.UtcNow;
        var consolidadoEsperado = new ConsolidadoDiario { Data = data, Saldo = 500m };

        _repositoryMock
            .Setup(r => r.ObterConsolidadoPorDataAsync(data))
            .ReturnsAsync(consolidadoEsperado);

        var resultado = await _service.ObterConsolidadoPorDataAsync(data);

        resultado.Should().NotBeNull();
        resultado.Should().BeEquivalentTo(consolidadoEsperado);
    }

    [Fact]
    public async Task ObterConsolidadoPorDataAsync_DeveRetornarNull_SeNaoExistir()
    {
        var data = DateTime.UtcNow;

        _repositoryMock
            .Setup(r => r.ObterConsolidadoPorDataAsync(data))
            .ReturnsAsync((ConsolidadoDiario?)null);

        var resultado = await _service.ObterConsolidadoPorDataAsync(data);

        resultado.Should().BeNull();
    }
}
