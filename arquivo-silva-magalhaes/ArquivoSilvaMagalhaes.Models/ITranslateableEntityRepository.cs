using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ArquivoSilvaMagalhaes.Models
{
    public interface ITranslateableEntityRepository<TEntity, TTranslation> : IRepository<TEntity>
        where TEntity : class
        where TTranslation : EntityTranslation
    {
        Task<IEnumerable<TTranslation>> GetAllByLanguage(string languageCode); 

        Task<TTranslation> GetTranslationAsync(int id, string languageCode);
        Task<TTranslation> GetTranslationOrDefault(int id, string languageCode);

        void AddTranslation(TTranslation translation);
        void RemoveTranslation(TTranslation translation);
        Task RemoveTranslationByIdAsync(params object[] keys);
        void UpdateTranslation(TTranslation translation);
    }
}
