using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels
{


    public class BannerPhotographEditViewModel
    {
        public Banner Banner { get; set; }

        [Required]
        [Display(ResourceType = typeof(BannerStrings), Name = "Image")]
        public HttpPostedFileBase Image { get; set; }
    }
}

