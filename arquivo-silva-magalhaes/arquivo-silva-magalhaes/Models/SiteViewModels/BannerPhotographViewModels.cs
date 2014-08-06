using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Resources.ModelTranslations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

