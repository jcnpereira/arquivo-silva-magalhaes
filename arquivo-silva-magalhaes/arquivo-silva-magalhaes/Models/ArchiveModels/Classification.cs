using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class Classification
    {
        public Classification()
        {
            this.ClassificationTexts = new HashSet<ClassificationText>();
            this.Specimens = new HashSet<Specimen>();
        }

        public int Id { get; set; }

        public virtual ICollection<ClassificationText> ClassificationTexts { get; set; }
        public virtual ICollection<Specimen> Specimens { get; set; }
    }

    public partial class ClassificationText
    {
        public ClassificationText()
        {
            this.LanguageCode = "pt";
        }

        public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string Value { get; set; }
        public int ClassificationId { get; set; }

        public virtual Classification Classification { get; set; }
    }
}