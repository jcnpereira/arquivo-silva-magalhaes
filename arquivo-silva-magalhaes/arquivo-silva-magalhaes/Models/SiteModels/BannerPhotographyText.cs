using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class BannerPhotographyText
    {
        public BannerPhotographyText()
        {
            CodBannerPhotography = new HashSet<BannerPhotography>();
        }
    
     
        public string CodLanguage;
        public string SubTitle;

        public virtual ICollection<BannerPhotography> CodBannerPhotography {get; set;}
    }
}