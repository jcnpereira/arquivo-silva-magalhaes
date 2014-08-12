using ArquivoSilvaMagalhaes.Resources;
using System.ComponentModel.DataAnnotations;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class Partnership
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Logo")]
        public string Logo { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "SiteLink")]
        public string SiteLink { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "EmailAddress")]
        public string EmailAddress { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Contact")]
        public string Contact { get; set; }

       // public int EventId { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "PartnershipType")]
        public PartnershipType PartnershipType { get; set; }
    }

    public enum PartnershipType : byte
    {
        Patrocinador = 1,
        Colaborador = 2,
        Outro = 100
    }
}