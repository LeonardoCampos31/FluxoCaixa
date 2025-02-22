

using FluxoCaixa.Consolidado.Modules.Entity;
using FluxoCaixa.Modules.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Consolidado.Modules.Repositories
{
    public interface IConsolidadoRepository
    {
        Task AtualizarConsolidadoAsync(DateTime data, decimal valor);
        Task<ConsolidadoDiario?> ObterConsolidadoPorDataAsync(DateTime data);
    }

    public class ConsolidadoRepository(DbContext context) : Repository<ConsolidadoDiario>(context), IConsolidadoRepository
    {

        public async Task AtualizarConsolidadoAsync(DateTime data, decimal valor)
        {
            var consolidado = await Set.AsQueryable().FirstOrDefaultAsync(x => x.Data.Date == data.Date);

            if (consolidado != null)
            {
                consolidado.Saldo += valor;
                Update(consolidado);
            }
            else
            {
                consolidado = new ConsolidadoDiario { Id = Guid.NewGuid(), Data = data.Date, Saldo = valor };
                InsertAsync(consolidado);
            }

            Context.SaveChanges();
        }

        public async Task<ConsolidadoDiario?> ObterConsolidadoPorDataAsync(DateTime data)
        {
            return await Set.AsQueryable().FirstOrDefaultAsync(x => x.Data.Date == data.Date);
        }
    }
}