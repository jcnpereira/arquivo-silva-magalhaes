//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Web;

//namespace ArquivoSilvaMagalhaes.Models.Repositories
//{
//    public class Repository<TEntity> where TEntity : class
//    {
//        internal ArchiveDataContext _db;
//        internal DbSet<TEntity> _dbSet;

//        public Repository()
//        {
//            _db = new ArchiveDataContext();
//            _dbSet = _db.Set<TEntity>();    
//        }

//        public Repository(ArchiveDataContext db)
//        {
//            this._db = db;
//            this._dbSet = _db.Set<TEntity>();
//        }

//        public virtual TEntity GetById(object id)
//        {
//            return _dbSet.Find(id);
//        }

//        public virtual IEnumerable<TEntity> Get(
//            Expression<Func<TEntity, bool>> filter = null,
    
//        )
//        {

//        }

//    }
//}