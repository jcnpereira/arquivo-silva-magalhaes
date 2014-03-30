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
            this.DocumentTexts = new HashSet<DocumentText>();
            this.Keywords = new HashSet<Keyword>();
            this.Specimens = new HashSet<Specimen>();
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The name of the person that is responsible for this collection.
        /// </summary>
        [Required]
        public string ResponsibleName { get; set; }

        /// <summary>
        /// The date on which this document was made.
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// The date on which this document was catalogued.
        /// </summary>
        public DateTime CatalogationDate { get; set; }

        /// <summary>
        /// Notes issued by the people in the archive about
        /// this document.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Code used by the people in the archive to
        /// physically catalog this document.
        /// </summary>
        public string CatalogCode { get; set; }

        /// <summary>
        /// The collection on which this document belongs to.
        /// </summary>
        public virtual Collection Collection { get; set; }
        public virtual ICollection<DocumentText> DocumentTexts { get; set; }

        [Required]
        public virtual Author Author { get; set; }
        public virtual ICollection<Keyword> Keywords { get; set; }
        public virtual ICollection<Specimen> Specimens { get; set; }
    }

    public partial class DocumentText
    {
        public DocumentText()
        {
            this.LanguageCode = "pt";
        }

        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }
        public string DocumentLocation { get; set; }
        public string FieldAndContents { get; set; }
        public string Description { get; set; }
        public int DocumentId { get; set; }

        [Required]
        public virtual Document Document { get; set; }
    }
}