using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Key]
        public int Id { get; set; }

        public virtual ICollection<ProcessText> ProcessTexts { get; set; }
        public virtual ICollection<Specimen> Specimens { get; set; }
    }

    public partial class ProcessText
    {
        [Key, Column(Order = 0)]
        public int ProcessId { get; set; }

        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }
        public string Value { get; set; }
    }
}