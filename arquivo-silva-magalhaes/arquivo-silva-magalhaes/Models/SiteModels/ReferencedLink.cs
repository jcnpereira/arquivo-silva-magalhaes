using ArquivoSilvaMagalhaes.Resources;
using System.ComponentModel.DataAnnotations;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class ReferencedLink
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [Display(ResourceType = typeof(DataStrings), Name = "ReferencedLink")]
        public string Link { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Description")]
        public string Description { get; set; }
    }
}