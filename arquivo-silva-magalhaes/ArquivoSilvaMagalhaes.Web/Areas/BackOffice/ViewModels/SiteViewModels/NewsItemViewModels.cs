using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels
{
    public class NewsItemViewModel
    {
        public NewsItem NewsItem { get; set; }

        [Display(ResourceType = typeof(NewsItemStrings), Name = "HeaderImage")]
        public HttpPostedFileBase HeaderImageUpload { get; set; }
    }
}
