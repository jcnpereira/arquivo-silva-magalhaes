using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Web.I18n;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels
{
    public class TechnicalDocumentEditViewModel
    {
        public TechnicalDocument TechnicalDocument { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "File")]
        public HttpPostedFileBase UploadedFile { get; set; }
    }
}