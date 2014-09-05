using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.ViewModels
{
    public class IndexViewModel : Banner
    {
        public IndexViewModel()
        {
            Translations = new List<BannerTranslation>();
        }

        [Key]
        public int Id { get; set; }

        [Display(ResourceType = typeof(BannerStrings), Name = "Image")]
        public string UriPath { get; set; }

        public virtual IList<BannerTranslation> Translations { get; set; }
        public virtual SpotlightVideo Video { get; set; }
    }

    public class BannerTranslation : EntityTranslation
    {
        [Key, Column(Order = 0)]
        public int BannerPhotographId { get; set; }
        [ForeignKey("BannerPhotographId")]
        public virtual Banner BannerPhotograph { get; set; }

        [Key, Column(Order = 1)]
        public override string LanguageCode { get; set; }

        [Display(ResourceType = typeof(BannerStrings), Name = "Caption")]
        public string Caption { get; set; }
    }

    public class SpotlightVideo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(ResourceType = typeof(SpotlightVideoStrings), Name = "UriPath")]
        public string UriPath { get; set; }
    }
}