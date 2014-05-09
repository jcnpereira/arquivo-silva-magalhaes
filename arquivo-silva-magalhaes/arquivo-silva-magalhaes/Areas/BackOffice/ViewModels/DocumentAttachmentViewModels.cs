using ArquivoSilvaMagalhaes.Models.SiteModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class DocumentAttachmentViewModels
    {
        public DocumentAttachmentViewModels()
        {
            EventsUsingAttachment = new HashSet<Event>();
            NewsUsingAttachment = new HashSet<NewsItem>();
            TextUsingAttachment = new HashSet<DocumentAttachmentText>();
        }

        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string MimeFormat { get; set; }
        [Required]
        public string UriPath { get; set; }
        [Required]
        public int Size { get; set; }

        public virtual ICollection<Event> EventsUsingAttachment { get; set; }
        public virtual ICollection<NewsItem> NewsUsingAttachment { get; set; }
        public virtual ICollection<DocumentAttachmentText> TextUsingAttachment { get; set; }
    }

    public class DocumentI18nPartialModels
    {
        public DocumentI18nPartialModels()
        {
            LanguageCode = "pt";
        }

        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public virtual DocumentAttachment DocumentAttachment { get; set; }
    }
}
