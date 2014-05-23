using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
  

    public class PortalViewModels
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ContactDetails { get; set; }
        [Required]
        public string Service { get; set; }
        [Required]
        public ArquivoSilvaMagalhaes.Models.SiteModels.LanguageCodes LanguageCode { get; set; }
        [Required]
        public string ArchiveHistory { get; set; }
        [Required]
        public string ArchiveMission { get; set; }


        public partial class PortalContactsViewModels
        {
            [Key]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public string Address { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            public string ContactDetails { get; set; }
            [Required]
            public string Service { get; set; }

            [Required]
            public PortalContactsViewModels Archive { get; set; }
        }


        public class ArchiveTexts
        {
         /*   public ArchiveTexts()
            {
                LanguageCode = "pt";
            }*/

            [Key]
            public int Id { get; set; }
            [Required]
            public string ArchiveHistory { get; set; }
            [Required]
            public string ArchiveMission { get; set; }

            public virtual PortalContactsViewModels Archive { get; set; }
            [Required]
            [Display(ResourceType = typeof(DataStrings), Name = "LanguageCode")]
            public ArquivoSilvaMagalhaes.Models.SiteModels.LanguageCodes LanguageCode { get; set; }
        }

    }
}