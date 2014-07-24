using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{
    public class DocumentEditViewModel
    {
        public Document Document { get; set; }

        public IEnumerable<SelectListItem> AvailableAuthors { get; set; }
        public IEnumerable<SelectListItem> AvailableCollections { get; set; }
    }
}
