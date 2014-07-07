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
            this.EventTexts = new List<EventTranslation>();
            ReferencedEvents = new HashSet<Event>();
            this.Partnerships = new List<Partnership>();
            Collaborators = new HashSet<Collaborator>();
            Links = new HashSet<ReferencedLink>();
            AttachedDocuments = new HashSet<Attachment>();
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
        [Display(ResourceType = typeof(DataStrings), Name = "EventType")]
        public EventType EventType { get; set; }

        public ICollection<Event> ReferencedEvents { get; set; }
        public List<Partnership> Partnerships { get; set; }
        public ICollection<Collaborator> Collaborators { get; set; }
        public ICollection<ReferencedLink> Links { get; set; }
        public ICollection<Attachment> AttachedDocuments { get; set; }

        public virtual List<EventTranslation> EventTexts { get; set; }
       
    }

    public partial class EventTranslation
    {

        [Key, Column(Order = 0)]
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }

        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }

        public string Title { get; set; }
        public string Heading { get; set; }
        public string SpotLight { get; set; }
        public string TextContent { get; set; }

    }
}