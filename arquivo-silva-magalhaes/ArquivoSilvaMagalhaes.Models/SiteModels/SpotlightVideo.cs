
    using ArquivoSilvaMagalhaes.Models.Translations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class SpotlightVideo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(ResourceType = typeof(SpotlightVideoStrings), Name = "UriPath")]
        public string UriPath { get; set;}
    }
}