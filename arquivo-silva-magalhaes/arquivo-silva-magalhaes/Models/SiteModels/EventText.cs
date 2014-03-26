using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public class EventText
    {

        public string CodLanguage{get; set;}
        public string Title { get; set; }
        public string Heading { get; set; }
        public string SpotLight {get; set;}
        public string TextContent { get; set; }
        public Enum Type { get; set; }

        public virtual ICollection<EventArgs> CodEvent { get; set; }

    }
}