﻿using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Web.I18n;
using System;
using System.ComponentModel.DataAnnotations;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels
{
    public class ReferencedLinkModels
    {
        [Key]
        [Required]
        public int Id { get; set; }
       
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }
        
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Link")]
        public string Link { get; set; }
        
        [Required]
        [DataType(DataType.MultilineText)]
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
        
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "EventsUsingThis")]
        public virtual Event EventsUsingThis { get; set; }
        
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "NewsUsingThis")]
        public virtual NewsItem NewsUsingThis { get; set; }
    }
}