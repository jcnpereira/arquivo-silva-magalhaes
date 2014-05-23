using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class ReferencedLink
    {

        public ReferencedLink()
        {
            this.EventsUsingThis = new HashSet<Event>();
            this.NewsUsingThis = new HashSet<NewsItem>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "ReferencedLink")]
        public string Link { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "DateOfCreation")]
        [DataType(DataType.Date)]
        public DateTime DateOfCreation { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "LastModifiedDate")]
        [DataType(DataType.Date)]
        public DateTime LastModifiedDate { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "IsUsefulLink")]
        public bool IsUsefulLink { get; set; }

        public ICollection<Event> EventsUsingThis { get; set; }
        public ICollection<NewsItem> NewsUsingThis { get; set; }
    }
}