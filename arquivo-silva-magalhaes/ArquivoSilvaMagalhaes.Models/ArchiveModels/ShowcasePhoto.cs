using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models.Translations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public class ShowcasePhoto : IValidatableObject
    {
        public ShowcasePhoto()
        {
            this.Translations = new List<ShowcasePhotoTranslation>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(ResourceType = typeof(ShowcasePhotoStrings), Name = "CommenterName")]
        public string CommenterName { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [Display(ResourceType = typeof(ShowcasePhotoStrings), Name = "CommenterEmail")]
        public string CommenterEmail { get; set; }

        [Display(ResourceType = typeof(ShowcasePhotoStrings), Name = "IsEmailVisible")]
        public bool IsEmailVisible { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(ShowcasePhotoStrings), Name = "VisibleSince")]
        public DateTime VisibleSince { get; set; }

        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(ShowcasePhotoStrings), Name = "HideAt")]
        public DateTime? HideAt { get; set; }

        [Display(ResourceType = typeof(ShowcasePhotoStrings), Name = "Image")]
        public int ImageId { get; set; }

        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }

        public virtual IList<ShowcasePhotoTranslation> Translations { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HideAt.HasValue && HideAt.Value < VisibleSince)
            {
                yield return new ValidationResult(ShowcasePhotoStrings.Validation_ExpiresBeforePublish, new string[] { "HideAt" });
            }
        }
    }

    public class ShowcasePhotoTranslation : EntityTranslation, IValidatableObject
    {
        [Key, Column(Order = 0)]
        public int ShowcasePhotoId { get; set; }

        [Key, Column(Order = 1), Required]
        public override string LanguageCode { get; set; }

        [Required]
        [StringLength(80)]
        [Display(ResourceType = typeof(ShowcasePhotoStrings), Name = "Title")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [Display(ResourceType = typeof(ShowcasePhotoStrings), Name = "Comment")]
        public string Comment { get; set; }

        [ForeignKey("ShowcasePhotoId")]
        public virtual ShowcasePhoto ShowcasePhoto { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Sanitize the html.
            Comment = HtmlEncoder.Encode(Comment, forbiddenTags: "script");

            return new List<ValidationResult>();
        }
    }
}