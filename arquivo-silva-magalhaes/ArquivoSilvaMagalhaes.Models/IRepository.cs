using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ArquivoSilvaMagalhaes.Models
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        /// <summary>
        ///     Adds an item to this repository.
        /// </summary>
        /// <param name="entity">
        ///     The item to add.
        /// </param>
        void Add(TEntity entity);

        /// <summary>
        ///     Marks an item to update in the repository.
        /// </summary>
        /// <param name="entity">
        ///     The item to update.
        /// </param>
        void Update(TEntity entity);

        /// <summary>
        ///     Marks an item to update in the repository, but excluding certain properties from the update.
        /// </summary>
        /// <param name="entity">
        ///     The item to update.
        /// </param>
        /// <param name="exclude">
        ///     A list of property names to exclude.
        /// </param>
        void Update(TEntity entity, params string[] exclude);

        /// <summary>
        ///     Removes an item from this repository.
        /// </summary>
        /// <param name="entity">
        ///     The item to remove.
        /// </param>
        void Remove(TEntity entity);

        /// <summary>
        ///     Asynchronously finds and removes an item from the repository.
        /// </summary>
        /// <param name="keys">
        ///     The key of the item.
        /// </param>
        Task RemoveByIdAsync(params object[] keys);

        /// <summary>
        ///     Gets the value of the specified property from the item from the db.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The type of the item.
        /// </typeparam>
        /// <param name="entity">
        ///     The item to get the value from.
        /// </param>
        /// <param name="expression">
        ///     An expression that extracts the property to get the value from.
        /// </param>
        /// <returns>
        ///     The value that is present in the db for the item.
        /// </returns>
        TResult GetValueFromDb<TResult>(TEntity entity, Expression<Func<TEntity, TResult>> expression)
            where TResult : class;

        /// <summary>
        ///     Marks an item to update in the repository, excluding certain properties from the update.
        /// </summary>
        /// <param name="entity">
        ///     The item to update.
        /// </param>
        /// <param name="exclude">
        ///     An expression that extracts the properties to exclude from the update.
        /// </param>
        void Update(TEntity entity, Expression<Func<TEntity, object>> exclude);

        /// <summary>
        ///     Excludes properties from update in the db for an item
        /// </summary>
        /// <param name="entity">
        ///     The item to exclude properties from.
        /// </param>
        /// <param name="exclude">
        ///     An expression that extracts the properties to exclude from the update.
        /// </param>
        void ExcludeFromUpdate(TEntity entity, Expression<Func<TEntity, object>> exclude);

        /// <summary>
        ///     Asynchronously gets all items from the repository.
        /// </summary>
        /// <returns>
        ///     All the items in the repository, or an empty collection if none exist.
        /// </returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        ///     Asynchronously gets an item from the repository.
        /// </summary>
        /// <param name="keys">
        ///     The key of the item to get.
        /// </param>
        /// <returns>
        ///     The item with the respective key, or null if none found.
        /// </returns>
        Task<TEntity> GetByIdAsync(params object[] keys);

        /// <summary>
        ///     Asynchronously queries the repository using the specified expression.
        /// </summary>
        /// <param name="condition">
        ///     An expression that filters the items.
        /// </param>
        /// <returns>
        ///     A collection of items that satisfy the condition. If none found, an empty collection
        ///     is returned.
        /// </returns>
        Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> condition);

        /// <summary>
        ///     Asynchronously saves all the changes to repository.
        /// </summary>
        /// <returns>
        ///     The number of items that were updated.
        /// </returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        ///     Saves all the changes to the repository.
        /// </summary>
        /// <returns>
        ///     The number of items that were updated.
        /// </returns>
        int SaveChanges();

        /// <summary>
        ///     Gets a collection of items of other type in the repository.
        /// </summary>
        /// <typeparam name="TOther">
        ///     The type of the items to get.
        /// </typeparam>
        /// <returns>
        ///     The respective collection.
        /// </returns>
        IQueryable<TOther> Set<TOther>() where TOther : class;

        /// <summary>
        ///     Forces the load of a many-to-many property from the db in order to make changes to it.
        /// </summary>
        /// <typeparam name="TOther">
        ///     The type of the entity of the navigation property.
        /// </typeparam>
        /// <param name="entity">
        ///     The entity from where to load the navigation property.
        /// </param>
        /// <param name="expression">
        ///     A lambda expression that selects the entity to load.
        /// </param>
        Task ForceLoadAsync<TOther>(TEntity entity, Expression<Func<TEntity, ICollection<TOther>>> expression) where TOther : class;

        void ForceUpdate<TResult>(TEntity entity, Expression<Func<TEntity, TResult>> expression);

        /// <summary>
        ///     Gets the underlying collection of entities from the repository.
        /// </summary>
        IQueryable<TEntity> Entities { get; }
    }
}