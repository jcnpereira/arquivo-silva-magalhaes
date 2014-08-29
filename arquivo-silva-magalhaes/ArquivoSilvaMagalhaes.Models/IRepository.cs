using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace ArquivoSilvaMagalhaes.Models
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Update(TEntity entity, params string[] exclude);
        void Remove(TEntity entity);
        Task RemoveByIdAsync(params object[] keys);

        TResult GetValueFromDb<TResult>(TEntity entity, Expression<Func<TEntity, TResult>> expression) where TResult : class;

        void Update(TEntity entity, Expression<Func<TEntity, object>> exclude);
        void ExcludeFromUpdate(TEntity entity, Expression<Func<TEntity, object>> exclude);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(params object[] keys);
        Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> condition);
        Task<int> SaveChangesAsync();

        int SaveChanges();

        IEnumerable<TOther> Set<TOther>() where TOther : class;

        /// <summary>
        /// Forces the load of a many-to-many property from the db
        /// in order to make changes to it.
        /// </summary>
        /// <typeparam name="TOther">The type of the entity of the navigation property.</typeparam>
        /// <param name="entity">The entity from where to load the navigation property.</param>
        /// <param name="expression">A lambda expression that selects the entity to load.</param>
        Task ForceLoadAsync<TOther>(TEntity entity, Expression<Func<TEntity, ICollection<TOther>>> expression) where TOther : class;
    }
}
