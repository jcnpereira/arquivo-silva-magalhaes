using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System.Collections.Generic;

namespace ArquivoSilvaMagalhaes.ViewModels
{
    /// <summary>
    /// ViewModel que contem os atributos dos autores aprensentados
    /// </summary>
    public class AuthorDetailsViewModel
    {
        public TranslatedViewModel<Author, AuthorTranslation> Author { get; set; }
        public IEnumerable<TranslatedViewModel<Document, DocumentTranslation>> Documents { get; set; }
        public IEnumerable<TranslatedViewModel<Collection, CollectionTranslation>> Collections { get; set; }
    }
}