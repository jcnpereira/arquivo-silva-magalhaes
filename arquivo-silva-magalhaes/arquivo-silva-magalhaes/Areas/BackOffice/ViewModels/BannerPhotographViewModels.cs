using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{


    public class BannerPhotographViewModels
    {

        /*    public BannerPhotographViewModels()
            {
                BannerTexts = new HashSet<BannerPhotographText>();
            }*/

        [Key]
        public int Id { get; set; }


        [Display(ResourceType = typeof(DataStrings), Name = "UriPath")]
        //public HttpPostedFileBase UriPath { get; set; }
        public string UriPath { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Photo:")]
        public byte[] ImageData { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "PublicationDate")]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "RemovalDate")]
        public DateTime RemovalDate { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "IsVisible")]
        public bool IsVisible { get; set; }

        //  public virtual ICollection<BannerPhotographText> BannerTexts { get; set; }


        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "LanguageCode")]
        public string LanguageCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "BannerTexts")]
        public string BannerTexts { get; set; }


        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }

    }

    public class BannerPhotographI18nEditModel
    {

        public BannerPhotographI18nEditModel()
        {
            LanguageCode = "pt";
        }


        public int Id { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "LanguageCode")]
        public string LanguageCode { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "BannerTexts")]
        public string BannerTexts { get; set; }
        // public virtual BannerPhotographViewModels Photograph { get; set; } 
    }
}

