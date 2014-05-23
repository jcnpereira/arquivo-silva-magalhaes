using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
  

    public class DocumentEditViewModel
    {
        [Key]
        public int Id { get; set; }
       
        [Required]
        public string MimeFormat { get; set; }
        [Required]
        public string UriPath { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string LanguageCode { get; set; }
    }
}