using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    

    public class BannerPhotographViewModels
    {
         
        public BannerPhotographViewModels()
        {
            BannerTexts = new HashSet<BannerPhotographText>();
        }

        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string UriPath { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [Required]
        public DateTime RemovalDate { get; set; }
        [Required]
        public bool IsVisible { get; set; }
        [Required]
        public virtual ICollection<BannerPhotographText> BannerTexts { get; set; }
    }

    public class BannerPhotographText
    {

        public BannerPhotographText()
        {
            LanguageCode = "pt";
        }

        [Key]
        [Required]
        public int Id { get; set; }
        
        [Key]
        [Required]
        public string LanguageCode { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description;

        public virtual BannerPhotographViewModels Photograph { get; set; }
    }
}

