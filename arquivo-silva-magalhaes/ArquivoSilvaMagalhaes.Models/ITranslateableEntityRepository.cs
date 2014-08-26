using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ArquivoSilvaMagalhaes.Models
{
    public class GenericTranslatedEntity<TEntity, TTranslation>
        where TEntity : class
        where TTranslation : EntityTranslation
    {
        public TEntity Entity { get; set; }
        public TTranslation Translation { get; set; }
    }

    public interface ITranslateableEntityRepository<TEntity, TTranslation> : IRepository<TEntity>
        where TEntity : class
        where TTranslation : EntityTranslation
    {
        Task<GenericTranslatedEntity<TEntity, TTranslation>> GetTranslatedEntity(int id, string languageCode);

        Task<IEnumerable<TTranslation>> GetAllByLanguage(string languageCode); 

        Task<TTranslation> GetTranslation(int id, string languageCode);
        Task<TTranslation> GetTranslationOrDefault(int id, string languageCode);

        void AddTranslation(TTranslation translation);
        void RemoveTranslation(TTranslation translation);
        void UpdateTranslation(TTranslation translation);
    }
}
