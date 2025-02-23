using FluentAssertions;
using FluxoCaixa.Consolidado.Modules.Controllers;
using FluxoCaixa.Consolidado.Modules.Entity;
using FluxoCaixa.Consolidado.Modules.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class ConsolidadoControllerTests
{
    private readonly Mock<IConsolidadoService> _serviceMock;
    private readonly ConsolidadoController _controller;

    public ConsolidadoControllerTests()
    {
        _serviceMock = new Mock<IConsolidadoService>();
        _controller = new ConsolidadoController(_serviceMock.Object);
    }

    [Fact]
    public async Task ObterConsolidadoPorData_DeveRetornarOk_ComConsolidado()
    {
        var data = DateTime.UtcNow;
        var consolidadoEsperado = new ConsolidadoDiario { Data = data, Saldo = 500m };

        _serviceMock
            .Setup(s => s.ObterConsolidadoPorDataAsync(data))
            .ReturnsAsync(consolidadoEsperado);

        var resultado = await _controller.ObterConsolidadoPorData(data);

        resultado.Should().BeOfType<OkObjectResult>();
        var okResult = resultado as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(consolidadoEsperado);
    }

    [Fact]
    public async Task ObterConsolidadoPorData_DeveRetornarOk_ComNull_QuandoNaoExistirConsolidado()
    {
        var data = DateTime.UtcNow;

        _serviceMock
            .Setup(s => s.ObterConsolidadoPorDataAsync(data))
            .ReturnsAsync((ConsolidadoDiario?)null);

        var resultado = await _controller.ObterConsolidadoPorData(data);

        resultado.Should().BeOfType<OkObjectResult>();
        var okResult = resultado as OkObjectResult;
        okResult!.Value.Should().BeNull();
    }
}
