using ArquivoSilvaMagalhaes.Models.Translations;
using ArquivoSilvaMagalhaes.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class NewsItem
    {
        public NewsItem()
        {
            Translations = new List<NewsItemTranslation>();
            Attachments = new List<Attachment>();

            LastModificationDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(NewsItemStrings), Name = "PublishDate")]
        public DateTime PublishDate { get; set; }

        
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(NewsItemStrings), Name = "ExpiryDate")]
        public DateTime? ExpiryDate { get; set; }

        [Required]
        [Display(ResourceType = typeof(NewsItemStrings), Name = "HideAfterExpiry")]
        public bool HideAfterExpiry { get; set; }
        
        [Display(ResourceType = typeof(NewsItemStrings), Name = "CreationDate")]
        public DateTime? CreationDate { get; set; }

        [Display(ResourceType = typeof(NewsItemStrings), Name = "LastModificationDate")]
        public DateTime LastModificationDate { get; set; }

        public virtual IList<NewsItemTranslation> Translations { get; set;}
        public virtual IList<Attachment> Attachments { get; set; }
    }

    public class NewsItemTranslation : EntityTranslation, IValidatableObject
    {
        [Key, Column(Order = 0)]
        public int NewsItemId { get; set; }
        [ForeignKey("NewsItemId")]
        public NewsItem NewsItem { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public override string LanguageCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(NewsItemStrings), Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(ResourceType = typeof(NewsItemStrings), Name = "Heading")]
        public string Heading { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [Display(ResourceType = typeof(NewsItemStrings), Name = "TextContent")]
        public string TextContent { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Sanitize the html.
            TextContent = HtmlEncoder.Encode(TextContent, forbiddenTags: "script");

            return new List<ValidationResult>();
        }
    }
}