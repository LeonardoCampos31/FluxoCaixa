using FluxoCaixa.Consolidado.Modules.Entity;
using FluxoCaixa.Consolidado.Modules.Repositories;

namespace FluxoCaixa.Consolidado.Modules.Services
{
    public interface IConsolidadoService
    {
        Task AtualizarConsolidadoAsync(DateTime data, decimal valor);
        Task<ConsolidadoDiario?> ObterConsolidadoPorDataAsync(DateTime data);
    }

    public class ConsolidadoService(IConsolidadoRepository repository) : IConsolidadoService
    {
        private readonly IConsolidadoRepository _repository = repository;

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