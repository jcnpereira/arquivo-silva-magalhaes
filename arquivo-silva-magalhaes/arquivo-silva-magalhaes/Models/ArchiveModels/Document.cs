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
            this.Specimen = new HashSet<Specimen>();
        }

        [Key]
        public int Id { get; set; }
        public string ResponsibleName { get; set; }
        public string DocumentDate { get; set; }
        public System.DateTime CatalogationDate { get; set; }
        public string Notes { get; set; }
        public int CollectionId { get; set; }
        public int AuthorId { get; set; }
        public string CatalogCode { get; set; }

        public virtual Collection Collection { get; set; }
        public virtual ICollection<DocumentText> DocumentTexts { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<Keyword> Keywords { get; set; }
        public virtual ICollection<Specimen> Specimen { get; set; }
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

        public virtual Document Document { get; set; }
    }
}