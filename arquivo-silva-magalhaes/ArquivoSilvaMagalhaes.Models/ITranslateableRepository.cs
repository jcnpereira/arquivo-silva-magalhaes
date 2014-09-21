using System.Threading.Tasks;
namespace ArquivoSilvaMagalhaes.Models
{
    public interface ITranslateableRepository<TEntity, TTranslation> : IRepository<TEntity>
        where TEntity : class
        where TTranslation : EntityTranslation
    {
        Task<TTranslation> GetTranslationAsync(int id, string languageCode);

        void AddTranslation(TTranslation translation);
        void RemoveTranslation(TTranslation translation);
        Task RemoveTranslationByIdAsync(params object[] keys);
        void UpdateTranslation(TTranslation translation);
    }
}
