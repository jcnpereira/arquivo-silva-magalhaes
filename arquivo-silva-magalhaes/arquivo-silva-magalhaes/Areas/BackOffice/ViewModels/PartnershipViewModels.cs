using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public enum PartnershipType : byte
    {
        [Display ()]
        Patrocinador = 1,
        Colaborador = 2,
        Outro = 100
    }

    public class PartnershipEditViewModels
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "Logo")]
        public HttpPostedFileBase Logo { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "SiteLink")]
        public string SiteLink { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "EmailAddress")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Contact")]
        public string Contact { get; set; }

        
        
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "PartnershipType")]
        public ArquivoSilvaMagalhaes.Models.SiteModels.PartnershipType PartnershipType { get; set; }

    }
}

