using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArquivoSilvaMagalhaes.Models
{
    interface IGenericRepository<TEntity> where TEntity : class
    {
        //public async Task<TEntity> GetById(object[] id);

        //public async Task<TEntity> GetTranslatedById(object[] id, string languageCode);

        //public async Task Add(TEntity entity);

        //public async Task Remove(TEntity entity);
        //public async Task RemoveTranslation(TEntity entity, string languageCode);

        //public async Task Update(TEntity entity);
        //public async Task UpdateTranslation(TEntity entity, string languageCode);
    }
}
