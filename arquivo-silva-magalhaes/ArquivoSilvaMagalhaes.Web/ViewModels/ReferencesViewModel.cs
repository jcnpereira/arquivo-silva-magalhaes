using ArquivoSilvaMagalhaes.Models.SiteModels;
using System.Collections.Generic;

namespace ArquivoSilvaMagalhaes.ViewModels
{
    public class ReferencesViewModel
    {
        public IEnumerable<TranslatedViewModel<ReferencedLink, ReferencedLinkTranslation>> ReferencedLinks { get; set; }
        public IEnumerable<TechnicalDocument> TechnicalDocuments { get; set; }
    }
}