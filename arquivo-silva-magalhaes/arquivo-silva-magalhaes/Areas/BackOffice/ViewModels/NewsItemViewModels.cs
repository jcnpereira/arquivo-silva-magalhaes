using ArquivoSilvaMagalhaes.Models.SiteModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class NewsItemViewModels
    {
     /*    public NewsItemViewModels()
        {
            Links = new HashSet<ReferencedLink>();
            ReferencedNewsItems = new HashSet<NewsItem>();
            ReferencedNewsText = new HashSet<NewsText>();
            ReferencedDocuments = new HashSet<DocumentAttachment>();
            HideAfterExpiry = false;
        }*/

        [Key]
        public int Id { get; set; }
       
        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public bool HideAfterExpiry { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime LastModificationDate { get; set; }

        public virtual ICollection<ReferencedLink> Links { get; set; }
        public virtual ICollection<NewsItem> ReferencedNewsItems { get; set; }
        public virtual ICollection<NewsText> ReferencedNewsText { get; set;}
        public virtual ICollection<DocumentAttachment> ReferencedDocuments { get; set; }


        [Required]
        public string LanguageCode { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string TextContent { get; set; }


    }

    public class NewsText
    {
        public NewsText()
        {
            LanguageCode = "pt";
        }

        [Key, Column(Order = 0)]
        [Required]
        public int Id { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string TextContent { get; set; }


        public virtual NewsItem NewsItem { get; set; }

    }
}
