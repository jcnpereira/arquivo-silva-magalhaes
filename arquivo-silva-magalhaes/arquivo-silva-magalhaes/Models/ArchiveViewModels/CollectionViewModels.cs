using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Utilitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{
    public partial class CollectionEditViewModel
    {
        public Collection Collection { get; set; }

        [Display(Name = "Autores desta coleção")]
        public IEnumerable<SelectListItem> AvailableAuthors { get; set; }

        //[ContentType(ContentType = "image/*", ErrorMessage = "A")]
        [Display(ResourceType = typeof(DataStrings), Name = "CollectionLogo"), Required]
        public HttpPostedFileBase Logo { get; set; }

        [Required]
        public int[] AuthorIds { get; set; }
    }
}