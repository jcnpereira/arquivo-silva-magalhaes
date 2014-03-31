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
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }

        public DateTime DateOfCreation { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public bool IsUsefulLink { get; set; }

        public virtual Event EventsUsingThis { get; set; }
        public virtual NewsItem NewsUsingThis { get; set; }
    }
}