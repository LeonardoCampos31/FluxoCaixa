using FluxoCaixa.Consolidado.Modules.Factory;
using FluxoCaixa.Lancamentos.Modules.Entity;
using FluxoCaixa.Lancamentos.Modules.Repositories;
using FluxoCaixa.Lancamentos.Modules.Services;
using Moq;

namespace FluxoCaixa.Lancamentos.Tests.Services
{
    public class LancamentoServiceTests
    {
        private readonly Mock<ILancamentoRepository> _repositoryMock;
        private readonly Mock<ILancamentoProducer> _producerMock;
        private readonly LancamentoService _service;

        public LancamentoServiceTests()
        {
            _repositoryMock = new Mock<ILancamentoRepository>();
            _producerMock = new Mock<ILancamentoProducer>();
            _service = new LancamentoService(_repositoryMock.Object, _producerMock.Object);
        }

        [Fact]
        public async Task AdicionarLancamentoAsync_DeveAdicionarLancamentoEPublicar()
        {
            decimal valor = 100;
            string tipo = "Credito";
            var lancamento = new Lancamento { Valor = valor, Tipo = tipo, Data = DateTime.UtcNow };

            _repositoryMock.Setup(r => r.AdicionarLancamentoAsync(It.IsAny<Lancamento>()))
                .Returns(Task.CompletedTask);
            _producerMock.Setup(p => p.PublicarLancamento(valor, tipo))
                .Returns(Task.CompletedTask);

            await _service.AdicionarLancamentoAsync(valor, tipo);

            _repositoryMock.Verify(r => r.AdicionarLancamentoAsync(It.Is<Lancamento>(l => l.Valor == valor && l.Tipo == tipo)), Times.Once);
            _producerMock.Verify(p => p.PublicarLancamento(valor, tipo), Times.Once);
        }

        [Fact]
        public async Task ObterLancamentosPorDataAsync_DeveRetornarListaLancamentos()
        {
            var data = DateTime.UtcNow.Date;
            var lancamentos = new List<Lancamento>
            {
                new Lancamento { Valor = 200, Tipo = "Credito", Data = data },
                new Lancamento { Valor = -100, Tipo = "Debito", Data = data }
            };

            _repositoryMock.Setup(r => r.ObterLancamentosPorDataAsync(data))
                .ReturnsAsync(lancamentos);

            var resultado = await _service.ObterLancamentosPorDataAsync(data);

            Assert.Equal(2, resultado.Count());
            _repositoryMock.Verify(r => r.ObterLancamentosPorDataAsync(data), Times.Once);
        }
    }
}