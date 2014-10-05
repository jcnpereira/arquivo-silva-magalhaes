using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System.Collections.Generic;

namespace ArquivoSilvaMagalhaes.ViewModels
{
    public class DocumentDetailsViewModel
    {
        public TranslatedViewModel<Collection, CollectionTranslation> Collection { get; set; }
        public TranslatedViewModel<Document, DocumentTranslation> Document { get; set; }
    }
}