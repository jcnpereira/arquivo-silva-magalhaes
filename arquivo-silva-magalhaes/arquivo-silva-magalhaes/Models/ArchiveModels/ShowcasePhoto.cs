using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public class ShowcasePhoto
    {
        public ShowcasePhoto()
        {
            this.Translations = new HashSet<ShowcasePhotoTranslation>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "ShowcasePhoto_CommenterName")]
        public string CommenterName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(ResourceType = typeof(DataStrings), Name = "ShowcasePhoto_CommenterEmail")]
        public string CommenterEmail { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "ShowcasePhoto_IsEmailVisible")]
        public string IsEmailVisible { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "PublicationDate")]
        public DateTime VisibleSince { get; set; }

        public int DigitalPhotographId { get; set; }

        [ForeignKey("DigitalPhotographId")]
        public DigitalPhotograph DigitalPhotograph { get; set; }

        public virtual ICollection<ShowcasePhotoTranslation> Translations { get; set; }
    }

    public class ShowcasePhotoTranslation
    {
        [Key, Column(Order = 0)]
        public int ShowcasePhotoId { get; set; }

        [Key, Column(Order = 1), Required]
        public string LanguageCode { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "ShowcasePhoto_Comment")]
        public string Comment { get; set; }

        [ForeignKey("ShowcasePhotoId")]
        public virtual ShowcasePhoto ShowcasePhoto { get; set; }
    }
}