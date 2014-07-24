using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
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

        [Display(ResourceType = typeof(DataStrings), Name = "CollectionLogo")]
        public HttpPostedFileBase Logo { get; set; }

        public int[] AuthorIds { get; set; }
    }
}