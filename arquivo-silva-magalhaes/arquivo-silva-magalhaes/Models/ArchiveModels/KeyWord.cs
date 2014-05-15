using ArquivoSilvaMagalhaes.Utilitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class Keyword
    {
        public Keyword()
        {
            this.KeywordTexts = new HashSet<KeywordText>();
            this.Documents = new HashSet<Document>();
            this.Specimens = new HashSet<Specimen>();
        }

        [Key]
        public int Id { get; set; }

        public ICollection<KeywordText> KeywordTexts { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<Specimen> Specimens { get; set; }
    }

    public partial class KeywordText
    {
        public KeywordText()
        {
            this.LanguageCode = "pt";
        }

        [Key, Column(Order = 0)]
        public int KeywordId { get; set; }

        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }

        public string Value { get; set; }

        [ForeignKey("KeywordId")]
        public Keyword Keyword { get; set; }
    }
}