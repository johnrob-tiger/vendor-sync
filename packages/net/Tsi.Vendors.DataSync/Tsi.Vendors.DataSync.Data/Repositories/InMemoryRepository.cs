using System.Linq.Expressions;
using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Interfaces;

namespace Tsi.Vendors.DataSync.Data.Repositories
{
    public abstract class InMemoryRepository<T, TK> : IAsyncRepository<T> where T : BaseEntity
    where TK : IComparable, IComparable<TK>
    {
        protected static List<T> Entities = new();
        
        protected abstract Func<T, TK> IdResolver { get; }
        
        protected virtual T AssignId(T obj)
        {
            // Subclass to assign an identity value
            return obj;
        }
        
        public Task<T> AddAsync(T entity)
        {
            entity = AssignId(entity);

            Entities.Add(entity);

            return Task.FromResult(entity);
        }

        public Task<T> UpdateAsync(T entity)
        {
            var found = false;

            for (var i = 0; i < Entities.Count; i++)
            {
                var current = Entities[i];
                if (current != entity) continue;

                Entities[i] = entity;
                found = true;
            }
            
            if (!found)
            {
                throw new EntityNotFoundException($"{typeof(T).Name} entity not found for id {IdResolver(entity)}.");
            }
            
            return Task.FromResult(entity);
        }

        public Task<bool> DeleteAsync(T entity)
        {
            var found = Entities.FirstOrDefault(x => x == entity);

            if (found == null)
            {
                throw new EntityNotFoundException($"{typeof(T).Name} entity not found for id {IdResolver(entity)}.");
            }

            Entities.Remove(entity);

            return Task.FromResult(true);
        }

        public Task<T?> GetAsync(Expression<Func<T, bool>> expression)
        {
            var entity = 
                Entities
                    .Where(expression.Compile())
                    .FirstOrDefault();  

            return Task.FromResult(entity);
        }

        public Task<List<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            var entities =
                Entities
                    .Where(expression.Compile());

            return Task.FromResult(entities.ToList());
        }
    }
}
