using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class BannerPhotography
    {
        public BannerPhotography(){
        BannerText = new HashSet<BannerPhotographyText>();
    }
        
        public string CodBannerPhotography { get; set; }

        public string Localization { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime RemotionDate { get; set; }
        public bool visible { get; set; }

        public virtual ICollection<BannerPhotographyText> BannerText { get; set; }
    }
}