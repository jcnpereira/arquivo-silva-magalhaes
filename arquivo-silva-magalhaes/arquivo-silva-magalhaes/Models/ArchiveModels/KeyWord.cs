using ArquivoSilvaMagalhaes.Resources;
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
            this.Translations = new HashSet<KeywordTranslation>();
            this.Documents = new HashSet<Document>();
            this.Specimens = new HashSet<Specimen>();
        }

        [Key]
        public int Id { get; set; }

        public virtual ICollection<KeywordTranslation> Translations { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Specimen> Specimens { get; set; }
    }

    public partial class KeywordTranslation
    {
        public KeywordTranslation()
        {
            this.LanguageCode = "pt";
        }

        [Key, Column(Order = 0)]
        public int KeywordId { get; set; }

        [Key, Column(Order = 1), Required]
        public string LanguageCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Keyword")]
        public string Value { get; set; }

        [ForeignKey("KeywordId")]
        public Keyword Keyword { get; set; }
    }
}