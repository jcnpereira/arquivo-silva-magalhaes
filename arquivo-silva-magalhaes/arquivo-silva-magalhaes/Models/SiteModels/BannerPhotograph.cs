using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class Banner
    {
        public Banner()
        {
            BannerTexts = new HashSet<BannerTranslation>();
        }

        [Key]
        public int Id { get; set; }
       
        public string UriPath { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime RemovalDate { get; set; }
        public bool IsVisible { get; set; }
        public string LanguageCode { get; set; }
        public string Title { get; set; }

        public ICollection<BannerTranslation> BannerTexts { get; set; }
    }

    public class BannerTranslation
    {

        [Key, Column(Order = 0)]
        public int BannerPhotographId { get; set; }
        [ForeignKey("BannerPhotographId")]
        public Banner BannerPhotograph { get; set; }

        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }

        public string Title { get; set; }
    }
}