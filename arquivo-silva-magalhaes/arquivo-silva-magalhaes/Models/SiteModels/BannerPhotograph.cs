using ArquivoSilvaMagalhaes.Resources;
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
        [Display(ResourceType = typeof(DataStrings), Name = "Image")]
        public string Image { get; set; }
        [Display(ResourceType = typeof(DataStrings), Name = "UriPath")]
        public string UriPath { get; set; }
        [Display(ResourceType = typeof(DataStrings), Name = "PublicationDate")]
        public DateTime PublicationDate { get; set; }
        [Display(ResourceType = typeof(DataStrings), Name = "RemovalDate")]
        public DateTime RemovalDate { get; set; }
        [Display(ResourceType = typeof(DataStrings), Name = "IsVisible")]
        public bool IsVisible { get; set; }
        [Display(ResourceType = typeof(DataStrings), Name = "LanguageCode")]
        public string LanguageCode { get; set; }
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
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