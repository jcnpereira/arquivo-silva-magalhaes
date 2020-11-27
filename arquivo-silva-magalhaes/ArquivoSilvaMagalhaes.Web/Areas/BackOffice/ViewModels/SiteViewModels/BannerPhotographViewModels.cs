using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels
{


    public class BannerPhotographEditViewModel
    {
        public BannerPhotographEditViewModel()
            : this(new Banner())
        {

        }

        public BannerPhotographEditViewModel(Banner banner)
        {
            this.Banner = banner;
        }

        public Banner Banner { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [Display(ResourceType = typeof(BannerStrings), Name = "Image")]
        public HttpPostedFileBase Image { get; set; }
    }
}

