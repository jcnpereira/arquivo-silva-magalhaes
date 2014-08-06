using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Resources.ModelTranslations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public class ShowcasePhoto
    {
        public ShowcasePhoto()
        {
            this.Translations = new List<ShowcasePhotoTranslation>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(ShowcasePhotoStrings), Name = "CommenterName")]
        public string CommenterName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(ResourceType = typeof(ShowcasePhotoStrings), Name = "CommenterEmail")]
        public string CommenterEmail { get; set; }

        [Display(ResourceType = typeof(ShowcasePhotoStrings), Name = "IsEmailVisible")]
        public bool IsEmailVisible { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(ShowcasePhotoStrings), Name = "VisibleSince")]
        public DateTime VisibleSince { get; set; }

        public int ImageId { get; set; }

        [ForeignKey("ImageId")]
        public Image Image { get; set; }

        public virtual IList<ShowcasePhotoTranslation> Translations { get; set; }
    }

    public class ShowcasePhotoTranslation
    {
        [Key, Column(Order = 0)]
        public int ShowcasePhotoId { get; set; }

        [Key, Column(Order = 1), Required]
        public string LanguageCode { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [Display(ResourceType = typeof(ShowcasePhotoStrings), Name = "Comment")]
        public string Comment { get; set; }

        [ForeignKey("ShowcasePhotoId")]
        public virtual ShowcasePhoto ShowcasePhoto { get; set; }
    }
}