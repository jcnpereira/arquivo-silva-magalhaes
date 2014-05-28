using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Resources;
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
         public NewsItemViewModels()
        {
            Links = new HashSet<ReferencedLink>();
            ReferencedNewsItems = new HashSet<NewsItem>();
            ReferencedNewsText = new HashSet<NewsI18nViewModel>();
            ReferencedDocuments = new HashSet<Attachment>();
            HideAfterExpiry = false;
        }

        [Key]
        public int Id { get; set; }
       
        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "PublishDate")]
        public DateTime PublishDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "ExpiryDate")]
        public DateTime ExpiryDate { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "HideAfterExpiry")]
        public bool HideAfterExpiry { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "CreationDate")]
        public DateTime CreationDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "LastModificationDate")]
        public DateTime LastModificationDate { get; set; }

        public virtual ICollection<ReferencedLink> Links { get; set; }
        public virtual ICollection<NewsItem> ReferencedNewsItems { get; set; }
        public virtual ICollection<NewsI18nViewModel> ReferencedNewsText { get; set;}
        public virtual ICollection<Attachment> ReferencedDocuments { get; set; }


        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "LanguageCode")]
        public string LanguageCode { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Subtitle")]
        public string Subtitle { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Heading")]
        public string Heading { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "TextContent")]
        public string TextContent { get; set; }


    }

    public class NewsI18nViewModel
    {
        public NewsI18nViewModel()
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
        [DataType(DataType.MultilineText)]
        public string TextContent { get; set; }


        public virtual NewsItem NewsItem { get; set; }

    }
}
