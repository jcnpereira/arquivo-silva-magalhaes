using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class Document
    {
        public Document()
        {
            this.PhotographicSpecimens = new HashSet<PhotographicSpecimen>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public System.DateTime DocumentDate { get; set; }
        public System.DateTime CatalogDate { get; set; }

        public string Notes { get; set; }
        public int CollectionId { get; set; }
        public int AuthorId { get; set; }

        public virtual Collection Collection { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<PhotographicSpecimen> PhotographicSpecimens { get; set; }
    }
}