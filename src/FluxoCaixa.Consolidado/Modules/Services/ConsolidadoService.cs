
using FluxoCaixa.Consolidado.Modules.Entity;
using FluxoCaixa.Consolidado.Modules.Repositories;

namespace FluxoCaixa.Consolidado.Modules.Services
{
    public class ConsolidadoService
    {
        private readonly IConsolidadoRepository _repository;

        public ConsolidadoService(IConsolidadoRepository repository)
        {
            _repository = repository;
        }

        public async Task AtualizarConsolidadoAsync(DateTime data, decimal valor)
        {
            await _repository.AtualizarConsolidadoAsync(data, valor);
        }

        public async Task<ConsolidadoDiario?> ObterConsolidadoPorDataAsync(DateTime data)
        {
            return await _repository.ObterConsolidadoPorDataAsync(data);
        }
    }
}