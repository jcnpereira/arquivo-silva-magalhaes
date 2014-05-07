using ArquivoSilvaMagalhaes.Models.SiteModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class ReferencedLinkModels
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DateOfCreation { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set; }
        [Required]
        public bool IsUsefulLink { get; set; }
        [Required]
        public virtual Event EventsUsingThis { get; set; }
        [Required]
        public virtual NewsItem NewsUsingThis { get; set; }
    }
}