using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
            AttachedDocuments = new HashSet<DocumentAttachment>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Heading { get; set; }

        [Required]
        public string TextContent { get; set; }
        public DateTime PublishDate { get; set; }

        
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(false)]
        public bool HideAfterExpiry { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }

        public virtual ICollection<ReferencedLink> Links { get; set; }
        public virtual ICollection<NewsItem> ReferencedNewsItems { get; set; }
        public virtual ICollection<DocumentAttachment> AttachedDocuments { get; set; }
    }
}