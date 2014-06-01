using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class Archive
    {
        public Archive()
        {
            this.ArchiveTranslations = new List<ArchiveTranslations>();
            this.Contact= new List<Contact>();
        //    ArchiveTexts = new HashSet<ArchiveTranslations>();
        //    Contacts = new HashSet<Contact>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Display (ResourceType=typeof(DataStrings), Name="Title")]
        public string Title { get; set; }

        [Required]
        [Display(ResourceType=typeof(DataStrings),Name="LanguageCode")]
        public string LanguageCode { get; set;}
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "ArchiveMission")]
        public string ArchiveMission { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "ArchiveHistory")]
        public string ArchiveHistory { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Name")]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "ContactDetails")]
        public string ContactDetails { get; set;}
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Service")]
        public string Service { get; set; }
        /// <summary>
        /// Localized texts descibing the mission and the history of this
        /// archive.
        /// </summary>
        public virtual List<ArchiveTranslations> ArchiveTranslations { get; set; }
        /// <summary>
        ///  Archive address and contact's
        /// </summary>
        public virtual List<Contact> Contact { get; set; }
     //   public ICollection<ArchiveTranslations> ArchiveTexts;
    //    public ICollection<Contact> Contacts;
    }

    public class ArchiveTranslations
    {
        [Key, Column(Order = 0)]
        public int ArchiveId { get; set; }
        [ForeignKey("ArchiveId")]
        public Archive Archive { get; set; }

        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }
        [Required]
        public string ArchiveHistory { get; set; }
        [Required]
        public string ArchiveMission { get; set; }
   }

    public class Contact
    {
        public int Id { get; set; }

        public string Name;
        public string Email;
        public string Address;
        public string ContactDetails;
        public string Service;

        [Required]
        public int ArchiveId { get; set; }

        [ForeignKey("ArchiveId")]
        public Archive Archive { get; set; }
    }
}