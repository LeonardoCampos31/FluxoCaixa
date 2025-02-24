using FluxoCaixa.Lancamentos.Controllers;
using FluxoCaixa.Lancamentos.Modules.DataTransfers;
using FluxoCaixa.Lancamentos.Modules.Entity;
using FluxoCaixa.Lancamentos.Modules.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class LancamentoControllerTests
{
    private readonly Mock<ILancamentoService> _serviceMock;
    private readonly LancamentoController _controller;

    public LancamentoControllerTests()
    {
        _serviceMock = new Mock<ILancamentoService>();
        _controller = new LancamentoController(_serviceMock.Object);
    }

    [Fact]
    public async Task AdicionarLancamento_DeveRetornarOk()
    {
        var request = new LancamentoRequest { Valor = 100, Tipo = "Credito" };
        _serviceMock.Setup(s => s.AdicionarLancamentoAsync(request.Valor, request.Tipo))
                    .Returns(Task.CompletedTask);

        var result = await _controller.AdicionarLancamento(request);

        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        _serviceMock.Verify(s => s.AdicionarLancamentoAsync(request.Valor, request.Tipo), Times.Once);
    }

    [Fact]
    public async Task ObterLancamentosPorData_DeveRetornarListaLancamentos()
    {
        var data = DateTime.UtcNow;
        var lancamentos = new List<Lancamento>
        {
            new Lancamento { Valor = 100, Tipo = "Credito", Data = data },
            new Lancamento { Valor = -50, Tipo = "Debito", Data = data }
        };

        _serviceMock.Setup(s => s.ObterLancamentosPorDataAsync(data))
                    .ReturnsAsync(lancamentos);

        var result = await _controller.ObterLancamentosPorData(data);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var returnedLancamentos = Assert.IsType<List<Lancamento>>(okResult.Value);
        Assert.Equal(2, returnedLancamentos.Count);
        _serviceMock.Verify(s => s.ObterLancamentosPorDataAsync(data), Times.Once);
    }
}