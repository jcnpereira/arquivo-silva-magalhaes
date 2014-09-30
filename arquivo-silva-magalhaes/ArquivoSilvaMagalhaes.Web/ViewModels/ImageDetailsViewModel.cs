using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System.Collections.Generic;

namespace ArquivoSilvaMagalhaes.ViewModels
{
    public class ImageDetailsViewModel
    {
        public TranslatedViewModel<Image, ImageTranslation> Image { get; set; }
        public TranslatedViewModel<Classification, ClassificationTranslation> Classification { get; set; }
        public IEnumerable<TranslatedViewModel<Keyword, KeywordTranslation>> Keywords { get; set; }
    }
}