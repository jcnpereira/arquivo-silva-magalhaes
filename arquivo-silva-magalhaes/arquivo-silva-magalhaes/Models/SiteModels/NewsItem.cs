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
            ReferencedNewsText = new HashSet<NewsItemTranslation>();
            ReferencedDocuments = new HashSet<Attachment>();
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

        public virtual ICollection<ReferencedLink> Links { get; set; }
        public virtual ICollection<NewsItem> ReferencedNewsItems { get; set; }
        public virtual ICollection<NewsItemTranslation> ReferencedNewsText { get; set;}
        public virtual ICollection<Attachment> ReferencedDocuments { get; set; }
    }

    public class NewsItemTranslation
    {
        [Key, Column(Order = 0)]
        public int NewsItemId { get; set; }
        [ForeignKey("NewsItemId")]
        public NewsItem NewsItem { get; set; }

        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string TextContent { get; set; }

    }
}