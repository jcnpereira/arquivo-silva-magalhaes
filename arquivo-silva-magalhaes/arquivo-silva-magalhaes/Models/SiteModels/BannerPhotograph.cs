using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Utilitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.WebPages.Html;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class BannerPhotograph
    {

        public BannerPhotograph()
        {
            this.SubTitle = new HashSet<BannerPhotographEditTexts>();
        }

        [Key]
        public int Id { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Photo:")]
        public byte[] ImageData { get; set; }

        [NotMapped]
        [Display(ResourceType = typeof(DataStrings), Name = "UriPath")]
        public string UriPath { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "PublicationDate")]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "RemovalDate")]
        [DataType(DataType.Date)]
        public DateTime RemovalDate { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "IsVisible")]
        public bool IsVisible { get; set; }

        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }

        public virtual ICollection<BannerPhotographEditTexts> SubTitle { get; set; }

        /*
                #region Non-mapped attributes
                [NotMapped]

                [Required]
                [Display(ResourceType = typeof(DataStrings), Name = "BannerTexts")]
                public string BannerTexts
                {
                    get
                    {
                        {
                            var bannerText = this.SubTitle
                                .First(text => Thread.CurrentThread.CurrentUICulture.ToString().Contains(text.LanguageCode) ||
                                    text.LanguageCode.Contains(LanguageDefinitions.DefaultLanguage));

                            return bannerText.BannerTexts;
                        }
                    }
                #endregion
                }

               */
        public partial class BannerPhotographEditTexts
        {

            /*   public BannerPhotographText()
               {
                   LanguageCode = "pt";
               }*/

            [Key]
            public int Id { get; set; }
            [Required]
            [Display(ResourceType = typeof(DataStrings), Name = "LanguageCode")]
            public string LanguageCode { get; set; }
            [Required]
            [Display(ResourceType = typeof(DataStrings), Name = "BannerTexts")]
            public string BannerTexts { get; set; }

            public virtual BannerPhotograph BannerPhotograph { get; set; }
        }
    }
}