using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class Keyword
    {
        public Keyword()
        {
            this.KeywordTexts = new HashSet<KeywordText>();
            this.Document = new HashSet<Document>();
        }

        public int Id { get; set; }

        public virtual ICollection<KeywordText> KeywordTexts { get; set; }
        public virtual ICollection<Document> Document { get; set; }
    }

    public partial class KeywordText
    {
        public KeywordText()
        {
            this.LanguageCode = "pt";
        }

        public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string Value { get; set; }
        public int KeywordId { get; set; }

        public virtual Keyword Keyword { get; set; }
    }
}