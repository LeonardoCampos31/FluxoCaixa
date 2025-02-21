
namespace FluxoCaixa.Lancamentos.Modules.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        T Find(object id);
        Task<T> FindAsync(object id);
        void Insert(T entity);
        void InsertAsync(T entity);
        void InsertRange(IEnumerable<T> entities);
        void InsertRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Delete(params object[] keyValues);
        void Delete(T entity);
        void Delete(object id);
        IQueryable<T> Queryable();
    }
}