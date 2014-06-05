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
            Partnerships = new HashSet<Partnership>();
            Collaborators = new HashSet<Collaborator>();
            Links = new HashSet<ReferencedLink>();
            AttachedDocuments = new HashSet<Attachment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Place { get; set; }
        public string Coordinates { get; set; }
        public string VisitorInformation { get; set; }


        public DateTime StartMoment { get; set; }
        public DateTime EndMoment { get; set; }


        public DateTime PublishDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public bool HideAfterExpiry { get; set; }

        /// <summary>
        /// The type of this event.
        /// </summary>
        public EventType EventType { get; set; }

        public ICollection<Event> ReferencedEvents { get; set; }
        public ICollection<Partnership> Partnerships { get; set; }
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