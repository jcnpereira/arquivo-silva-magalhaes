


using ArquivoSilvaMagalhaes.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class ClassificationViewModel
    {
        public int Id { get; set; }
        public string Classification { get; set; }
    }

    public class ClassificationEditModel
    {
        public int Id { get; set; }

        [Required]
        [Display(ResourceType=typeof(DataStrings), Name="Classification")]
        public string Classfication { get; set; }

        [Required]
        [Display(ResourceType=typeof(DataStrings), Name="Language")]
        public string LanguageCode { get; set; }
        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }
    }
}