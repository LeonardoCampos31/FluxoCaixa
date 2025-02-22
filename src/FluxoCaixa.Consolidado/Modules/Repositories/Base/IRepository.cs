
namespace FluxoCaixa.Lancamentos.Modules.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        T Find(object id);
        Task<T> FindAsync(object id);
        void InsertAsync(T entity);
        void InsertRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(object id);
        IQueryable<T> Queryable();
    }
}