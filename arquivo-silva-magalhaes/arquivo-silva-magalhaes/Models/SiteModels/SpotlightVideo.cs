using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class SpotlightVideo
    {
        [Key]
        public int Id { get; set; }
        public string UriPath { get; set;}
        public DateTime PublicationDate { get; set; }
        public DateTime RemotionDate { get; set; }
        public bool IsPermanent { get; set; }
    }
}