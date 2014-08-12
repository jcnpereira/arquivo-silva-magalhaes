using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Resources.ModelTranslations;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteViewModels
{


    public class BannerPhotographEditViewModel
    {
        public Banner Banner { get; set; }

        [Required]
        [Display(ResourceType = typeof(BannerStrings), Name = "Image")]
        public HttpPostedFileBase Image { get; set; }
    }
}

