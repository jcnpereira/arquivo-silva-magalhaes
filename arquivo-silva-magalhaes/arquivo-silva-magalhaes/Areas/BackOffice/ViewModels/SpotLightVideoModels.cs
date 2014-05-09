using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class SpotLightVideoModels
    {

        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string UriPath { get; set;}
        [Required]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime RemotionDate { get; set; }
        [Required]
        public bool IsPermanent { get; set; }
    
    }

}