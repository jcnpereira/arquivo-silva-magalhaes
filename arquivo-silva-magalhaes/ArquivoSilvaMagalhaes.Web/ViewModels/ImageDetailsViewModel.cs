using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System.Collections.Generic;

namespace ArquivoSilvaMagalhaes.ViewModels
{
    public class ImageDetailsViewModel
    {
        public TranslatedViewModel<Image, ImageTranslation> Image { get; set; }
        public TranslatedViewModel<Classification, ClassificationTranslation> Classification { get; set; }
        public IEnumerable<TranslatedViewModel<Keyword, KeywordTranslation>> Keywords { get; set; }

        public IEnumerable<TranslatedViewModel<Specimen, SpecimenTranslation>> Specimens { get; set; }
        public IEnumerable<TranslatedViewModel<Process, ProcessTranslation>> Processes { get; set; }
        public IEnumerable<Format> Formats { get; set; }
    }
}