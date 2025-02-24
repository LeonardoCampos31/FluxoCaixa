using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Lancamentos.Modules.Repositories.Base
{
    public class Repository<T>(DbContext context) : IDisposable, IRepository<T> where T : class
    {
        protected readonly DbContext Context = context;
        protected readonly DbSet<T> Set = context.Set<T>();

        public virtual T Find(object id)
        {
            return Set.Find(id) ?? throw new InvalidOperationException("Entity not found");
        }

        public virtual async Task<T> FindAsync(object id)
        {
            return await Set.FindAsync(id) ?? throw new InvalidOperationException("Entity not found");
        }

        public virtual void InsertAsync(T entity)
        {
            Context.AddAsync(entity);
        }

        public virtual void InsertRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                InsertAsync(entity);
            }
        }

        public virtual void Update(T entity)
        {
            Context.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        public virtual void Delete(T entity)
        {
            Context.Remove(entity);
        }

        public virtual void Delete(object id)
        {
            var entity = Set.Find(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public IQueryable<T> Queryable() => Set;

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}