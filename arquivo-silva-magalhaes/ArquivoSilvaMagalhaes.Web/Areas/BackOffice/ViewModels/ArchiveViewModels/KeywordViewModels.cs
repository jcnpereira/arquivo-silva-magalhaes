using ArquivoSilvaMagalhaes.Models.Translations;
using System.ComponentModel.DataAnnotations;
namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels
{
    public class KeywordViewModel
    {
        public int Id { get; set; }
        [Required, Display(ResourceType = typeof(KeywordStrings), Name = "Value")]
        public string Value { get; set; }
    }

    //public class KeywordEditViewModel
    //{
    //    public int Id { get; set; }

    //    public int KeywordId { get; set; }

    //    [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
    //    public string LanguageCode { get; set; }

    //    [Required, Display(ResourceType = typeof(DataStrings), Name = "Keyword")]
    //    public string Value { get; set; }

    //    [NotMapped]
    //    public IEnumerable<SelectListItem> AvailableLanguages { get; set; }
    //}
}