using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System.Collections.Generic;

namespace ArquivoSilvaMagalhaes.ViewModels
{
    public class CollectionDetailsViewModel
    {
        public TranslatedViewModel<Collection, CollectionTranslation> Collection { get; set; }
        public IList<TranslatedViewModel<Document, DocumentTranslation>> Documents { get; set; }
        public IList<TranslatedViewModel<Author, AuthorTranslation>> Authors { get; set; }
    }
}