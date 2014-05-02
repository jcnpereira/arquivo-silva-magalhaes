using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class KeywordViewModel
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
    }

    public class KeywordEditModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Keyword { get; set; }

        [Required]
        public string LanguageCode { get; set; }

        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }
    }
}