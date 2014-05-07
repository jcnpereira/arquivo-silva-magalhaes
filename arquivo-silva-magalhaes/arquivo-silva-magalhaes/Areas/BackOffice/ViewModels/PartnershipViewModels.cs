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
        Sponsor = 1,
        Collaborator = 2,
        Other = 100
    }

    public class PartnershipEditViewModels :IValidatableObject
    {

        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Logo { get; set; }
        [Required]
        public string SiteLink { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public virtual ArquivoSilvaMagalhaes.Models.SiteModels.Event Event { get; set; }
        
        
        [Required]
        public PartnershipType PartnershipType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }

    

}

