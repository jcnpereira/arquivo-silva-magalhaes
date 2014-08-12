using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class DocumentAttachmentViewModels
    {
        public DocumentAttachmentViewModels()
        {
            EventsUsingAttachment = new HashSet<Event>();
            NewsUsingAttachment = new HashSet<NewsItem>();
            TextUsingAttachment = new HashSet<AttachmentTranslation>();
        }

        [Key]
        [Required]
        public int Id { get; set; }
        [Display(ResourceType = typeof(DataStrings))]
        public string MimeFormat { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "UriPath")]
        public String UriPath { get; set; }
        [NotMapped]
        [Display(ResourceType = typeof(DataStrings), Name = "Logo")]
        public HttpPostedFileBase Logo { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Size")]
        public int Size { get; set; }
        [Display(ResourceType = typeof(DataStrings), Name = "Language")]
        public string LanguageCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }
        
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Description")]
        public string Description { get; set; }

        public virtual ICollection<Event> EventsUsingAttachment { get; set; }
        public virtual ICollection<NewsItem> NewsUsingAttachment { get; set; }
        public virtual ICollection<AttachmentTranslation> TextUsingAttachment { get; set; }

        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }

    }

    public class DocumentI18nPartialModels
    {
        public DocumentI18nPartialModels()
        {
            LanguageCode = "pt";
        }

        [Key]
        public int Id { get; set; }
        [Display(ResourceType = typeof(DataStrings), Name = "Language")]
        public string LanguageCode { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Description")]
        public string Description { get; set; }

        public virtual Attachment DocumentAttachment { get; set; }
    }
}
