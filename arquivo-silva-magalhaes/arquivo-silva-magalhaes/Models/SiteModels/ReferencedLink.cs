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

        public virtual Event EventsUsingThis { get; set; }
        public virtual NewsItem NewsUsingThis { get; set; }
    }
}