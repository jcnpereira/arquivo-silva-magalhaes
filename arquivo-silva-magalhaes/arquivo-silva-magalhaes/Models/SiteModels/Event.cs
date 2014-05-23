using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public enum EventType : byte
    {
        Exposição = 1,
        Escolar = 2,
        Outro = 100
    }

    public class Event
    {
        public Event()
        {
            ReferencedEvents = new HashSet<Event>();
            Partnerships = new HashSet<Partnership>();
            Collaborators = new HashSet<Collaborator>();
            Links = new HashSet<ReferencedLink>();
            AttachedDocuments = new HashSet<DocumentAttachment>();
            EventTexts = new HashSet<EventText>();


            HideAfterExpiry = false;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(ResourceType = typeof(DataStrings), Name = "Place")]
        public string Place { get; set; }
        [Display(ResourceType = typeof(DataStrings), Name = "Coordinates")]
        public string Coordinates { get; set; }
        [Display(ResourceType = typeof(DataStrings), Name = "VisitorInformation")]
        public string VisitorInformation { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "StartMoment")]
        public DateTime StartMoment { get; set; }
        [Display(ResourceType = typeof(DataStrings), Name = "EndMoment")]
        public DateTime EndMoment { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "PublishDate")]
        public DateTime PublishDate { get; set; }
        [Display(ResourceType = typeof(DataStrings), Name = "ExpiryDate")]
        public DateTime ExpiryDate { get; set; }
        [Display(ResourceType = typeof(DataStrings), Name = "HideAfterExpiry")]
        public bool HideAfterExpiry { get; set; }

        /// <summary>
        /// The type of this event.
        /// </summary>
        /// 
        [Display(ResourceType = typeof(DataStrings), Name = "EventType")]
        public EventType EventType { get; set; }

        public virtual ICollection<Event> ReferencedEvents { get; set; }
        public virtual ICollection<Partnership> Partnerships { get; set; }
        public virtual ICollection<Collaborator> Collaborators { get; set; }
        public virtual ICollection<ReferencedLink> Links { get; set; }
        public virtual ICollection<DocumentAttachment> AttachedDocuments { get; set; }
        public virtual ICollection<EventText> EventTexts { get; set; }
    }

    public partial class EventText
    {
        public EventText()
        {
            LanguageCode = "pt";
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "LanguageCode")]
        public string LanguageCode { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Heading")]
        public string Heading { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "SpotLight")]
        public string SpotLight { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "TextContent")]
        public string TextContent { get; set; }

       // [Required]
       // public int EventId { get; set; }

        [Required]
        public virtual Event Event { get; set; }

    }
}