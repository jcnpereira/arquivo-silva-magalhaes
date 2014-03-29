using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class BannerPhotograph
    {
        public BannerPhotograph()
        {
            BannerTexts = new HashSet<BannerPhotographText>();
        }

        [Key]
        public int Id { get; set; }

        public string UriPath { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime RemovalDate { get; set; }
        public bool IsVisible { get; set; }

        public virtual ICollection<BannerPhotographText> BannerTexts { get; set; }
    }

    public class BannerPhotographText
    {

        public BannerPhotographText()
        {
            LanguageCode = "pt";
        }

        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Description;

        public virtual BannerPhotograph Photograph { get; set; }
    }
}