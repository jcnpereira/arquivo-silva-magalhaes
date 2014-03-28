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
            NewsUsingAttachment = new HashSet<NewsItem>();
            TextUsingAttachment = new HashSet<DocumentAttachmentText>();
        }

        public int Id { get; set; }
        public string Format { get; set; }

        public virtual ICollection<Event> EventsUsingAttachment { get; set; }
        public virtual ICollection<NewsItem> NewsUsingAttachment { get; set; }
        public virtual ICollection<DocumentAttachmentText> TextUsingAttachment { get; set; }
    }
}