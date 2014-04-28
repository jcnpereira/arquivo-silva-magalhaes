using System.ComponentModel.DataAnnotations;
namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class KeywordViewModel
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
    }

    public class KeywordEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Keyword { get; set; }

        [Required]
        public string LanguageCode { get; set; }
    }
}