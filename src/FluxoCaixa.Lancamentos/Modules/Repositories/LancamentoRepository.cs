using FluxoCaixa.Lancamentos.Modules.Models;
using FluxoCaixa.Lancamentos.Modules.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Lancamentos.Modules.Repositories
{
    public interface ILancamentoRepository
    {
        Task AdicionarLancamentoAsync(Lancamento lancamento);
        Task<IEnumerable<Lancamento>> ObterLancamentosPorDataAsync(DateTime data);
    }

    public class LancamentoRepository(DbContext context) : Repository<Lancamento>(context), ILancamentoRepository
    {
        public async Task AdicionarLancamentoAsync(Lancamento lancamento)
        {
            lancamento.Id = Guid.NewGuid();
            Set.Add(lancamento);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Lancamento>> ObterLancamentosPorDataAsync(DateTime data)
        {
            return await Set.AsQueryable()
                                 .Where(l => l.Data.Date == data.Date)
                                 .ToListAsync();
        }
    }
}