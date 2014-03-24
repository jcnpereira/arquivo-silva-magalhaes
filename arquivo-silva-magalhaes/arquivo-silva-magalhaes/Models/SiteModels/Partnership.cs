using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class Partnership
    {
        public Partnership()
        {
            EventsAsPartner = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string SiteLink { get; set; }
        public string EmailAddress { get; set; }
        public string Contact { get; set; }

        public virtual ICollection<Event> EventsAsPartner { get; set; }
    }
}