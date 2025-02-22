using FluxoCaixa.Consolidado.Modules.Factory;
using FluxoCaixa.Lancamentos.Modules.Models;
using FluxoCaixa.Lancamentos.Modules.Repositories;

namespace FluxoCaixa.Lancamentos.Modules.Services
{
    public class LancamentoService
    {
        private readonly ILancamentoRepository _repository;

        private readonly LancamentoProducer _producer;

        public LancamentoService(ILancamentoRepository repository, LancamentoProducer producer)
        {
            _repository = repository;
            _producer = producer;
        }

        public async Task AdicionarLancamentoAsync(decimal valor, string tipo)
        {
            valor = tipo.Equals("Debito") ? -valor : valor;
            var lancamento = new Lancamento
            {
                Valor = valor,
                Tipo = tipo,
                Data = DateTime.UtcNow
            };
            await _repository.AdicionarLancamentoAsync(lancamento);

            await _producer.PublicarLancamento(valor, tipo);
        }

        public async Task<IEnumerable<Lancamento>> ObterLancamentosPorDataAsync(DateTime data)
        {
            return await _repository.ObterLancamentosPorDataAsync(data);
        }
    }
}