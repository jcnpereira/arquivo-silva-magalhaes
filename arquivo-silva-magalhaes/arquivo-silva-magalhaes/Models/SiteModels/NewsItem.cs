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
            ReferencedNewsText = new HashSet<NewsTexts>();
            ReferencedDocuments = new HashSet<DocumentAttachment>();

            HideAfterExpiry = false;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public bool HideAfterExpiry { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime LastModificationDate { get; set; }





        [Required]
        public string LanguageCode { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string TextContent { get; set; }

        public virtual ICollection<ReferencedLink> Links { get; set; }
        public virtual ICollection<NewsItem> ReferencedNewsItems { get; set; }
        public virtual ICollection<NewsTexts> ReferencedNewsText { get; set;}
        public virtual ICollection<DocumentAttachment> ReferencedDocuments { get; set; }
    }

    public class NewsTexts
    {
        public NewsTexts()
        {
            LanguageCode = "pt";
        }

        [Key]
        public int Id { get; set; }
    
        [Required]
        public string LanguageCode { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string TextContent { get; set; }

        
        public virtual NewsItem NewsItem { get; set; }

    }
}