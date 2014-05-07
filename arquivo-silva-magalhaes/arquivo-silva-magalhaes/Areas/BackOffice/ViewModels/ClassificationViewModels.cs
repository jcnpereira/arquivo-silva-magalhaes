


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class ClassificationEditModel
    {
        public int Id { get; set; }

        [Required]
        public string Classfication { get; set; }

        public string LanguageCode { get; set; }
        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }
    }
}