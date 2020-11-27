using System.ComponentModel.DataAnnotations;

namespace ArquivoSilvaMagalhaes.Models
{
    public class AppConfiguration
    {
        public const string VideoUrlKey = "videourl";

        [Key]
        [MaxLength(200)]
        public string Key { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        public string Value { get; set; }
    }
}