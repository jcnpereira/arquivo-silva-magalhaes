using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace ArquivoSilvaMagalhaes.Models
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task RemoveById(params object[] keys);

        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(params object[] keys);
        Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> condition);
        Task<int> SaveChanges();
        
    }
}
