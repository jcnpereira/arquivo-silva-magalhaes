using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class Process
    {
        public Process()
        {
            this.ProcessTexts = new HashSet<ProcessText>();
            this.Specimens = new HashSet<Specimen>();
        }

        public int Id { get; set; }

        public virtual ICollection<ProcessText> ProcessTexts { get; set; }
        public virtual ICollection<Specimen> Specimens { get; set; }
    }

    public partial class ProcessText
    {
        public ProcessText()
        {
            this.LanguageCode = "pt";
        }

        public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string Value { get; set; }
        public int ProcessId { get; set; }

        public virtual Process Process { get; set; }
    }
}