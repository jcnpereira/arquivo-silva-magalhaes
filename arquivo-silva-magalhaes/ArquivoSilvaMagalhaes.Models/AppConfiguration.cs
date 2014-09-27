using System.ComponentModel.DataAnnotations;

namespace ArquivoSilvaMagalhaes.Models
{
    public class AppConfiguration
    {
        public const string VideoUrlKey = "videourl";

        [Key]
        [MaxLength(200)]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }
    }
}