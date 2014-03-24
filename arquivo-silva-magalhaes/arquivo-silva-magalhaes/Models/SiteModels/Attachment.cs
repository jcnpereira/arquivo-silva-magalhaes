using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class DocumentAttachment
    {
        public DocumentAttachment()
        {
            EventsUsingAttachment = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Format { get; set; }

        public virtual ICollection<Event> EventsUsingAttachment { get; set; }

    }
}