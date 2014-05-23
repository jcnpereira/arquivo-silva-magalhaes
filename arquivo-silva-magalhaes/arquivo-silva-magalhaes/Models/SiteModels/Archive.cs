using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{

    public enum LanguageCodes : byte
    {
        pt=1,
        en=2,
        outro=3  
    }

    public class Archive
    {
        public Archive()
        {
            ArchiveTexts = new HashSet<ArchiveText>();
            Contacts = new HashSet<Contact>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }
        public ICollection<ArchiveText> ArchiveTexts;
        public ICollection<Contact> Contacts;

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "LanguageCode")]
        public LanguageCodes LanguageCode { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "ArchiveHistory")]
        public string ArchiveHistory { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "ArchiveMission")]
        public string ArchiveMission { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Name")]
        public string Name { get; set;}
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "EmailAddress")]
        public string Email { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "ContactDetails")]
        public string ContactDetails { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Service")]
        public string Service { get; set; }
       




    }

    public class ArchiveText
    {
       /* public ArchiveText()
        {
            LanguageCode = "pt";
        }*/

        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "LanguageCode")]
        public LanguageCodes LanguageCode { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "ArchiveHistory")]
        public string ArchiveHistory { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "ArchiveMission")]
        public string ArchiveMission { get; set; }

        public virtual Archive Archive { get; set; }


    }

    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ContactDetails { get; set; }
        [Required]
        public string Service { get; set; }
       
        public virtual Archive Archive { get; set; }
    }
}