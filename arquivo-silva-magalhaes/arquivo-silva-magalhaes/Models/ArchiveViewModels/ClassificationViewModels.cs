


using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{
    public class ClassificationViewModel
    {
        public int Id { get; set; }
        [Required, Display(ResourceType = typeof(DataStrings), Name = "Classification")]
        public string Value { get; set; }
    }

    public class ClassificationEditViewModel
    {
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