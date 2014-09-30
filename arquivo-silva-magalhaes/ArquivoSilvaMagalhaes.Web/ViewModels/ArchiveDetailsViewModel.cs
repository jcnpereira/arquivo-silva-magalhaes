using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System.Collections.Generic;

namespace ArquivoSilvaMagalhaes.ViewModels
{
    public class ArchiveDetailsViewModel
    {
        public IEnumerable<TranslatedViewModel<Author, AuthorTranslation>> Authors { get; set; }

        public IEnumerable<TranslatedViewModel<Collection, CollectionTranslation>> Collections { get; set; }
    }
}