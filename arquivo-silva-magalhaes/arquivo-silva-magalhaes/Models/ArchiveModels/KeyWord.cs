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

        public virtual ICollection<KeywordText> KeywordTexts { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Specimen> Specimens { get; set; }

        [NotMapped]
        public string Value
        {
            get
            {
                return KeywordTexts.First(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage).Value;
            }
        }
    }

    public partial class KeywordText
    {
        public KeywordText()
        {
            this.LanguageCode = "pt";
        }

        [Key]
        public int Id { get; set; }

        public string LanguageCode { get; set; }
        public string Value { get; set; }

        [Required]
        public virtual Keyword Keyword { get; set; }
    }
}