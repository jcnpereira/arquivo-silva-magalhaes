using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class ArchiveContact
    {
        public string Name;
        public string Email;
        public string Address;
        public string Contact;
        public string Service;

        public ICollection<PhotographicArchive> CodArchive { get; set; }
    }
}