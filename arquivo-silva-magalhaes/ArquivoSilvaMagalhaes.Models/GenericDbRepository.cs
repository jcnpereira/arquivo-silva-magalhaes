using ArquivoSilvaMagalhaes.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models
{
    public class GenericDbRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected ArchiveDataContext _db;

        public GenericDbRepository(ArchiveDataContext db)
        {
            this._db = db;
        }

        public GenericDbRepository()
            : this(new ArchiveDataContext())
        {
        }

        public void Add(TEntity entity)
        {
            _db.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity> GetByIdAsync(params object[] keys)
        {
            return await _db.Set<TEntity>().FindAsync(keys);
        }

        public async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await _db.Set<TEntity>().Where(condition).ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _db.Set<TEntity>().ToListAsync();
        }

        public async Task RemoveByIdAsync(params object[] keys)
        {
            var set = _db.Set<TEntity>();
            var entity = await set.FindAsync(keys);

            if (entity != null)
            {
                set.Remove(entity);
            }
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
            }
        }

        public IQueryable<TOther> Set<TOther>() where TOther : class
        {
            return _db.Set<TOther>();
        }

        public async Task ForceLoadAsync<TOther>(TEntity entity, Expression<Func<TEntity, ICollection<TOther>>> expression) where TOther : class
        {
            _db.Set<TEntity>().Attach(entity);
            await _db.Entry(entity).Collection(expression).LoadAsync();
        }

        public void Update(TEntity entity, params string[] exclude)
        {
            var entry = _db.Entry(entity);

            foreach (var propToExclude in exclude)
            {
                entry.Property(propToExclude).IsModified = false;
            }

            entry.State = EntityState.Modified;
        }

        public void Update(TEntity entity, Expression<Func<TEntity, object>> exclude)
        {
            ExcludeFromUpdate(entity, exclude);

            _db.Entry(entity).State = EntityState.Modified;
        }

        public TResult GetValueFromDb<TResult>(TEntity entity, Expression<Func<TEntity, TResult>> expression)
            where TResult : class
        {
            var entry = _db.Entry(entity);

            return entry.GetDatabaseValues()
                        .GetValue<TResult>(ExpressionHelper.GetExpressionText(expression));
        }

        public void ExcludeFromUpdate(TEntity entity, Expression<Func<TEntity, object>> exclude)
        {
            var entry = _db.Entry(entity);

            var t = exclude.Body.Type;
            var props = t.GetProperties().Select(p => p.Name).ToList();

            foreach (var prop in props)
            {
                entry.Property(prop).IsModified = false;
            }
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public IQueryable<TEntity> Entities
        {
            get
            {
                return _db.Set<TEntity>();
            }
        }


        public void ForceUpdate<TResult>(TEntity entity, Expression<Func<TEntity, TResult>> expression)
        {
            _db.Entry(entity).Property(expression).IsModified = true;
        }
    }

    public class TranslateableGenericRepository<TEntity, TTranslation> : GenericDbRepository<TEntity>, ITranslateableRepository<TEntity, TTranslation>
        where TEntity : class
        where TTranslation : EntityTranslation
    {
        public async Task<TTranslation> GetTranslationAsync(int id, string languageCode)
        {
            return await _db.Set<TTranslation>().FindAsync(id, languageCode);
        }

        public async Task<TTranslation> GetTranslationOrDefault(int id, string languageCode)
        {
            var translation = await _db.Set<TTranslation>().FindAsync(id, languageCode);

            return translation ?? await _db.Set<TTranslation>().FindAsync(id, LanguageDefinitions.DefaultLanguage);
        }

        public void AddTranslation(TTranslation translation)
        {
            _db.Set<TTranslation>().Add(translation);
        }

        public void RemoveTranslation(TTranslation translation)
        {
            _db.Set<TTranslation>().Remove(translation);
        }

        public void UpdateTranslation(TTranslation translation)
        {
            _db.Entry(translation).State = EntityState.Modified;
        }

        public async Task<IEnumerable<TTranslation>> GetAllByLanguage(string languageCode)
        {
            return await _db.Set<TTranslation>().Where(t => t.LanguageCode == languageCode).ToListAsync();
        }

        public async Task RemoveTranslationByIdAsync(params object[] keys)
        {
            var t = _db.Set<TTranslation>();

            t.Remove(await t.FindAsync(keys));
        }
    }
}