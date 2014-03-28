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
        [Display(ResourceType = typeof(SiteModelStrings), Name = "EventTypeSchool")]
        School,
        [Display(ResourceType = typeof(SiteModelStrings), Name = "EventTypeExpo")]
        Expo,
        [Display(ResourceType = typeof(SiteModelStrings), Name = "EventTypeOther")]
        Other
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
            EventText = new HashSet<EventText>();
        }




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

        [DefaultValue(false)]
        public bool HideAfterExpiry { get; set; }

        /// <summary>
        /// The type of this event.
        /// </summary>
        public EventType EventType { get; set; }

        public virtual ICollection<Event> ReferencedEvents { get; set; }
        public virtual ICollection<Partnership> Partnerships { get; set; }
        public virtual ICollection<Collaborator> Collaborators { get; set; }
        public virtual ICollection<ReferencedLink> Links { get; set; }
        public virtual ICollection<DocumentAttachment> AttachedDocuments { get; set; }
        public virtual ICollection<EventText> EventText { get; set; }


        
    }
}