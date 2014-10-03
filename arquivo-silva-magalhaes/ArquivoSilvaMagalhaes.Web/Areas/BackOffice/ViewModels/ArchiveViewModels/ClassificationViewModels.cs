


using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels
{
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

        [Required, Display(ResourceType = typeof(ClassificationStrings), Name = "Value")]
        public string Value { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }
    }
}