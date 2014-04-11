using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class DocumentViewModel
    {
    }

    public class DocumentEditViewModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }

        public string AuthorName { get; set; }
        public IEnumerable<SelectListItem> Authors { get; set; }

        [Required]
        public string ResponsibleName { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DocumentDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CatalogationDate { get; set; }

        [Required]
        public string Notes { get; set; }

        [Required]
        public string CatalogCode { get; set; }

        // PORTUGUESE

        [Required]
        public string DocumentLocationPt { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string FieldAndContentsPt { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string DescriptionPt { get; set; }

        // ENGLISH
        [Required]
        public string DocumentLocationEn { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string FieldAndContentsEn { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string DescriptionEn { get; set; }
    }
}