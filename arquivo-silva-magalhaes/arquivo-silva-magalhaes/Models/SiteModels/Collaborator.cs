using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class Collaborator
    {
        public Collaborator()
        {
            EventsAsCollaborator = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Task { get; set; }
        public bool EmailVisible { get; set; }
        public string Contact { get; set; }

        public virtual ICollection<Event> EventsAsCollaborator { get; set; }
    }
}