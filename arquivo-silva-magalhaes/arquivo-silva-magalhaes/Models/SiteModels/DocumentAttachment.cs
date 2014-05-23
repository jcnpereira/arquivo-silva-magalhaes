using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Utilitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public partial class DocumentAttachment
    {
        public DocumentAttachment()
        {
            EventsUsingAttachment = new HashSet<Event>();
            NewsUsingAttachment = new HashSet<NewsItem>();
            TextUsingAttachment = new HashSet<DocumentAttachmentText>();
        }

        [Key]
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

        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }

        #region Non-mapped attributes
        [NotMapped]
        [Display(ResourceType = typeof(DataStrings), Name = "LanguageCode")]
        public string LanguageCode
        {
            get
            {
                return LanguageCode;
            }
        }

        [NotMapped]
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title {get; set;}
     /*   {
              get
              {
                  var docText = this.TextUsingAttachment
                          .First(text => Thread.CurrentThread.CurrentUICulture.ToString().Contains(text.LanguageCode) ||
                              text.LanguageCode.Contains(LanguageDefinitions.DefaultLanguage));
                  return docText.Title;
                  
              }
        }*/

        [NotMapped]
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Description")]
        public string Description{get; set;}
        /*{
             get
             {
                 var docText = this.TextUsingAttachment
                         .First(text => Thread.CurrentThread.CurrentUICulture.ToString().Contains(text.LanguageCode) ||
                             text.LanguageCode.Contains(LanguageDefinitions.DefaultLanguage));
                 return docText.Description;
                 
             } 
         }*/
        #endregion
    }

    public partial class DocumentAttachmentText
    {
        public DocumentAttachmentText()
        {
            LanguageCode = "pt";
        }

        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }

        public virtual DocumentAttachment DocumentAttachment { get; set; }
    }
}