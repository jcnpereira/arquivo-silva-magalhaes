using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class NewsItem
    {
        public NewsItem()
        {
            Links = new HashSet<ReferencedLink>();
            ReferencedNewsItems = new HashSet<NewsItem>();
            ReferencedNewsText = new HashSet<NewsText>();
            ReferencedDocuments = new HashSet<DocumentAttachment>();

            HideAfterExpiry = false;
        }

        [Key]
        public int Id { get; set; }
       
        public DateTime PublishDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool HideAfterExpiry { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }

        public virtual ICollection<ReferencedLink> Links { get; set; }
        public virtual ICollection<NewsItem> ReferencedNewsItems { get; set; }
        public virtual ICollection<NewsText> ReferencedNewsText { get; set;}
        public virtual ICollection<DocumentAttachment> ReferencedDocuments { get; set; }
    }

    public class NewsText
    {
        public NewsText()
        {
            LanguageCode = "pt";
        }

        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Heading { get; set; }
        public string TextContent { get; set; }


        public virtual NewsItem NewsItem { get; set; }

    }
}