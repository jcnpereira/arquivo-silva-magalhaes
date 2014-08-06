using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System;
using System.Threading.Tasks;

namespace ArquivoSilvaMagalhaes.Models
{
    public class KeywordRepository : IGenericRepository<Keyword>
    {
        ArchiveDataContext _db;

        public KeywordRepository() : this(new ArchiveDataContext()) { }

        public KeywordRepository(ArchiveDataContext db)
        {
            _db = db;
        }

        public async Task<Keyword> GetById(object[] id)
        {
            return await _db.Keywords.FindAsync(id);
        }

        public async System.Threading.Tasks.Task<Keyword> GetTranslatedById(object[] id, string languageCode)
        {


            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task Add(Keyword entity)
        {
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task Remove(Keyword entity)
        {
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task RemoveTranslation(Keyword entity, string languageCode)
        {
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task Update(Keyword entity)
        {
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task UpdateTranslation(Keyword entity, string languageCode)
        {
            throw new NotImplementedException();
        }
    }
}