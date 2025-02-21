using FluxoCaixa.Lancamentos.Modules.Models;
using FluxoCaixa.Lancamentos.Modules.Repositories;

namespace FluxoCaixa.Lancamentos.Modules.Services
{
    public class LancamentoService
    {
        private readonly ILancamentoRepository _repository;

        public LancamentoService(ILancamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task AdicionarLancamentoAsync(decimal valor, string tipo)
        {
            var lancamento = new Lancamento
            {
                Valor = valor,
                Tipo = tipo,
                Data = DateTime.UtcNow
            };
            await _repository.AdicionarLancamentoAsync(lancamento);
        }

        public async Task<IEnumerable<Lancamento>> ObterLancamentosPorDataAsync(DateTime data)
        {
            return await _repository.ObterLancamentosPorDataAsync(data);
        }
    }
}