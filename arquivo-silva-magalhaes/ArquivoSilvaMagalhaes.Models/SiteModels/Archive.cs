using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ArquivoSilvaMagalhaes.Models.Translations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class Archive
    {
        public Archive()
        {
            Translations = new List<ArchiveTranslation>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(ArchiveStrings), Name = "Address")]
        public string Address { get; set; }

        public virtual IList<ArchiveTranslation> Translations { get; set; }
    }

    public class ArchiveTranslation : EntityTranslation
    {
        [Key, Column(Order = 0)]
        public int ArchiveId { get; set; }
        [ForeignKey("ArchiveId")]
        public virtual Archive Archive { get; set; }

        [Key, Column(Order = 1)]
        public override string LanguageCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(ArchiveStrings), Name = "ArchiveHistory")]
        public string ArchiveHistory { get; set; }

        [Required]
        [Display(ResourceType = typeof(ArchiveStrings), Name = "ArchiveMission")]
        public string ArchiveMission { get; set; }
    }

    public class Contact
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(ResourceType = typeof(ContactStrings), Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }
        
        [Required]
        [Display(ResourceType = typeof(ContactStrings), Name = "PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}