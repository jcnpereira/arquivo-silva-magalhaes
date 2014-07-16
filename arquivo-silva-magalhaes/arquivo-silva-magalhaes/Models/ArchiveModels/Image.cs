using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Resources;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public class Image
    {
        public Image()
        {
            Keywords = new HashSet<Keyword>();
            Specimens = new HashSet<Specimen>();
        }

        public int Id { get; set; }

        public string Title { get; set; }
        public string Subject { get; set; }

        public DateTime ProductionDate { get; set; }

        public string Publication { get; set; }
        public string Location { get; set; }

        

        public string Description { get; set; }

        public string ImageCode { get; set; }

        [ForeignKey("DocumentId")]
        public Document Document { get; set; }
        public int DocumentId { get; set; }

        public virtual IEnumerable<Keyword> Keywords { get; set; }
        public virtual IEnumerable<Specimen> Specimens { get; set; }

        public int? DigitalPhotographId { get; set; }
        [ForeignKey("DigitalPhotographId")]
        public DigitalPhotograph DigitalPhotograph { get; set; }

    }
}