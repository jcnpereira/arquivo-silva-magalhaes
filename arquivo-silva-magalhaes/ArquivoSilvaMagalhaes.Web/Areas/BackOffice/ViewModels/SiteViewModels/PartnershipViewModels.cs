using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels
{
    public class PartnershipEditViewModel : IValidatableObject
    {
        public Partnership Partnership { get; set; }

        [Display(ResourceType = typeof(PartnershipStrings), Name = "Logo")]
        public HttpPostedFileBase Upload { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Upload != null && !Upload.ContentType.ToLower().StartsWith("image/"))
            {
                yield return new ValidationResult(PartnershipStrings.Validation_LogoMustBeImage, new string[] { "Upload" });
            }
        }
    }
}