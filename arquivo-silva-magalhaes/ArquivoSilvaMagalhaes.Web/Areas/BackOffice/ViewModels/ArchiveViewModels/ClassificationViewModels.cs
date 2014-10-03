


using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Web.I18n;
using ArquivoSilvaMagalhaes.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels
{
    public class ClassificationViewModel
    {
        public ClassificationViewModel()
        {

        }

        public ClassificationViewModel(Classification c)
        {
            this.Id = c.Id;
            this.Value = c.Translations
                .FirstOrDefault(ct => ct.LanguageCode == LanguageDefinitions.DefaultLanguage).Value;
        }


        public int Id { get; set; }
        [Required, Display(ResourceType = typeof(DataStrings), Name = "Classification")]
        public string Value { get; set; }
    }

    public class ClassificationEditViewModel
    {
        public ClassificationEditViewModel()
        {

        }

        public ClassificationEditViewModel(
            Classification c, 
            string languageCode = LanguageDefinitions.DefaultLanguage,
            IEnumerable<string> availableLanguages = null)
        {
            availableLanguages = availableLanguages ?? new List<string>();

            Id = c.Id;
        }

        public int Id { get; set; }

        public int ClassificationId { get; set; }

        [Required]
        public string LanguageCode { get; set; }

        [Required, Display(ResourceType = typeof(DataStrings), Name = "Classification")]
        public string Value { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }
    }
}