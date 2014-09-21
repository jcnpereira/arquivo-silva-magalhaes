using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.ViewModels
{
    /// <summary>
    /// ViewModel que contem atributos do model banner utilizados na página de indexView do arquivo
    /// </summary>
    public class IndexViewModel
    {
        [Key]
        public int Id { get; set; }

        public string UriPath { get; set; }
        public string Caption { get; set; }

        public string Video { get; set; }

    }
}