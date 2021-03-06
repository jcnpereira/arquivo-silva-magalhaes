﻿using ArquivoSilvaMagalhaes.Models.Translations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class Banner
    {
        public Banner()
        {
            Translations = new List<BannerTranslation>();
        }

        [Key]
        public int Id { get; set; }

        [Display(ResourceType = typeof(BannerStrings), Name = "Image")]
        public string UriPath { get; set; }

        public virtual IList<BannerTranslation> Translations { get; set; }
    }

    public class BannerTranslation : EntityTranslation
    {
        [Key, Column(Order = 0)]
        public int BannerPhotographId { get; set; }

        [ForeignKey("BannerPhotographId")]
        public virtual Banner BannerPhotograph { get; set; }

        [Key, Column(Order = 1)]
        public override string LanguageCode { get; set; }

        [StringLength(40)]
        [Display(ResourceType = typeof(BannerStrings), Name = "Caption")]
        public string Caption { get; set; }
    }
}