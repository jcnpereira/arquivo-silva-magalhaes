using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class Attachment
    {
        public Attachment()
        {
            EventsUsingAttachment = new HashSet<Event>();
            NewsUsingAttachment = new HashSet<NewsItem>();
            TextUsingAttachment = new HashSet<AttachmentTranslation>();
        }

        [Key]
        public int Id { get; set; }
        public string MimeFormat { get; set; }
        public string UriPath { get; set; }

        public string Title { get; set; }

        public int Size { get; set; }

        public ICollection<Event> EventsUsingAttachment { get; set; }
        public ICollection<NewsItem> NewsUsingAttachment { get; set; }
        public ICollection<AttachmentTranslation> TextUsingAttachment { get; set; }
    }

    public class AttachmentTranslation
    {
        [Key, Column(Order = 0)]
        public int DocumentAttachmentId { get; set; }
        [ForeignKey("DocumentAttachmentId")]
        public Attachment DocumentAttachment { get; set; }

        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}