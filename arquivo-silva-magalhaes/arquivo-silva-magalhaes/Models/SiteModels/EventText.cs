using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class EventText
    {
        public EventText()
        {
            CodEvent = new HashSet<Event>();
        }

        public string CodLanguage{get; set;}
        public string Title { get; set; }
        public string Heading { get; set; }
        public string SpotLight {get; set;}
        public string TextContent { get; set; }
        public Enum Type { get; set; }

        public virtual ICollection<Event> CodEvent { get; set; }

    }
}