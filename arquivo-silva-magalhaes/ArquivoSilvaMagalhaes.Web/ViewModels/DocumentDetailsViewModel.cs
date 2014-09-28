using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System.Collections.Generic;

namespace ArquivoSilvaMagalhaes.ViewModels
{
    public class DocumentDetailsViewModel
    {
        public TranslatedViewModel<Document, DocumentTranslation> Document { get; set; }
        public IEnumerable<TranslatedViewModel<Image, ImageTranslation>> Images { get; set; }
    }
}