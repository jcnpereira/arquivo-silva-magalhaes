using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models.Translations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public enum EventType : byte
    {
        [Display(ResourceType = typeof(EventStrings), Name = "EventType_Expo")]
        Expo = 1,
        [Display(ResourceType = typeof(EventStrings), Name = "EventType_School")]
        School = 2,
        [Display(ResourceType = typeof(EventStrings), Name = "EventType_Workshop")]
        Workshop = 3,
        [Display(ResourceType = typeof(EventStrings), Name = "EventType_Other")]
        Other = 100
    }

    public class Event : IValidatableObject
    {
        public Event()
        {
            Translations = new List<EventTranslation>();
            Partnerships = new List<Partnership>();
            Attachments = new List<Attachment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(ResourceType = typeof(EventStrings), Name = "Place")]
        public string Place { get; set; }

        [Display(ResourceType = typeof(EventStrings), Name = "Latitude")]
        //public string Coordinates { get; set; }
        public string Latitude { get; set; }
        [Display(ResourceType = typeof(EventStrings), Name = "Longitude")]
        public string Longitude { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(EventStrings), Name = "VisitorInformation")]
        public string VisitorInformation { get; set; }

        [Required]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(ResourceType = typeof(EventStrings), Name = "StartMoment")]
        public DateTime StartMoment { get; set; }

        [Required]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [Display(ResourceType = typeof(EventStrings), Name = "EndMoment")]
        public DateTime EndMoment { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(EventStrings), Name = "PublishDate")]
        public DateTime PublishDate { get; set; }

        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(EventStrings), Name = "ExpiryDate")]
        public DateTime? ExpiryDate { get; set; }

        [Display(ResourceType = typeof(EventStrings), Name = "HideAfterExpiry")]
        public bool HideAfterExpiry { get; set; }

        /// <summary>
        /// The type of this event.
        /// </summary>
        [Required]
        [Display(ResourceType = typeof(EventStrings), Name = "EventType")]
        public EventType? EventType { get; set; }

        public virtual IList<Partnership> Partnerships { get; set; }
        public virtual IList<Attachment> Attachments { get; set; }
        public virtual IList<EventTranslation> Translations { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ExpiryDate < PublishDate)
            {
                yield return new ValidationResult(EventStrings.Validation_ExpiresBeforePublish, new string[] { "ExpiryDate" });
            }

            if (EndMoment > StartMoment)
            {
                yield return new ValidationResult(EventStrings.Validation_EndsBeforeItStarts, new string[] { "EndMoment" });
            }
        }
    }

    public partial class EventTranslation : EntityTranslation, IValidatableObject
    {

        [Key, Column(Order = 0)]
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }

        [Key, Column(Order = 1)]
        public override string LanguageCode { get; set; }

        [Required]
        [StringLength(70)]
        [Display(ResourceType = typeof(EventStrings), Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(EventStrings), Name = "Heading")]
        public string Heading { get; set; }

        [Required]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Display(ResourceType = typeof(EventStrings), Name = "TextContent")]
        public string TextContent { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Sanitize the html.
            TextContent = HtmlEncoder.Encode(TextContent, forbiddenTags: "script");

            return new List<ValidationResult>();
        }
    }
}