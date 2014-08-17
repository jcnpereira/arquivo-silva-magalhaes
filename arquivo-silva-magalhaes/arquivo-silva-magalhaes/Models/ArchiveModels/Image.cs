using ArquivoSilvaMagalhaes.Resources.ModelTranslations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public class Image : IValidatableObject
    {
        public Image()
        {
            Keywords = new List<Keyword>();
            Specimens = new List<Specimen>();
            Translations = new List<ImageTranslation>();
            ShowcasePhotos = new List<ShowcasePhoto>();
        }

        public int Id { get; set; }

        [Display(ResourceType = typeof(ImageStrings), Name = "ProductionDate")]
        [DataType(DataType.Date)]
        public DateTime? ProductionDate { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        [RegularExpression("^[A-Za-z0-9]+-[A-Za-z0-9]+-[A-Za-z0-9]+$", ErrorMessageResourceType = typeof(ImageStrings), ErrorMessageResourceName = "CodeFormat")]
        [Display(ResourceType = typeof(ImageStrings), Name = "ImageCode")]
        public string ImageCode { get; set; }

        public string ImageUrl { get; set; }

        [ForeignKey("DocumentId")]
        [Display(ResourceType = typeof(ImageStrings), Name = "Document")]
        public virtual Document Document { get; set; }
        [Display(ResourceType = typeof(ImageStrings), Name = "Document")]
        public int DocumentId { get; set; }

        public virtual IList<ImageTranslation> Translations { get; set; }
        [Display(ResourceType = typeof(ImageStrings), Name = "Keywords")]
        public virtual IList<Keyword> Keywords { get; set; }
        public virtual IList<Specimen> Specimens { get; set; }

        public virtual IList<ShowcasePhoto> ShowcasePhotos { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (var _db = new ArchiveDataContext())
            {
                if (_db.Images.Any(i => i.ImageCode == this.ImageCode && i.Id != this.Id))
                {
                    yield return new ValidationResult(ImageStrings.CodeAlreadyExists, new string[] { "ImageCode" });
                }
            }
        }
    }

    public class ImageTranslation
    {
        [Key, Column(Order = 0)]
        public int ImageId { get; set; }
        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(ImageStrings), Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(ResourceType = typeof(ImageStrings), Name = "Subject")]
        public string Subject { get; set; }

        [Display(ResourceType = typeof(ImageStrings), Name = "Publication")]
        public string Publication { get; set; }

        [Required]
        [Display(ResourceType = typeof(ImageStrings), Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Display(ResourceType = typeof(ImageStrings), Name = "Description")]
        public string Description { get; set; }

        [ForeignKey("ImageId")]
        public Image Image { get; set; }
    }
}