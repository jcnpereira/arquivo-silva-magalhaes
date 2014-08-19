using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Data.Entity;
using ArquivoSilvaMagalhaes.Common;
using System.ComponentModel.DataAnnotations;

namespace ArquivoSilvaMagalhaes.ViewModels
{
    public class CollectionViewModel : FrontOfficeViewModel
    {
        public CollectionViewModel(Collection c)
        {
            var translation = c.Translations.FirstOrDefault(t => t.LanguageCode == Thread.CurrentThread.CurrentUICulture.Name);

            if (translation == null)
            {
                LocalizationWarningMsg = "Lamentamos, mas não é .......";
                translation = c.Translations.FirstOrDefault(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage);
            }

            this.Collection = c;
            Translation = translation;
            var Title =Translation.Title;
            var Provenience = Translation.Provenience;
            var Description = Translation.Description;
            var Dimension = Translation.Dimension;
            var Authors = Collection.Authors;
            var Type = Collection.Type;
        }

        public Collection Collection { get; set; }
        public CollectionTranslation Translation { get; set; }
    }
}