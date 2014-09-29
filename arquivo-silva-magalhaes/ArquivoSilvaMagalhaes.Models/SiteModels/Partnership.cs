using ArquivoSilvaMagalhaes.Models.Translations;
using System.ComponentModel.DataAnnotations;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class Partnership
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        [Display(ResourceType = typeof(PartnershipStrings), Name = "Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(PartnershipStrings), Name = "Logo")]
        public string LogoFileName { get; set; }

        [DataType(DataType.Url)]
        [Display(ResourceType = typeof(PartnershipStrings), Name = "SiteLink")]
        public string SiteLink { get; set; }

        [Display(ResourceType = typeof(PartnershipStrings), Name = "EmailAddress")]
        public string EmailAddress { get; set; }

        [Display(ResourceType = typeof(PartnershipStrings), Name = "Contact")]
        public string Contact { get; set; }
    }
}