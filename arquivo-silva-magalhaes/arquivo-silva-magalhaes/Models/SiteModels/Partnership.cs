using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class Partnership
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string SiteLink { get; set; }
        public string EmailAddress { get; set; }
        public string Contact { get; set; }

        public int EventId { get; set; }

        public PartnershipType PartnershipType { get; set; }
    }

    public enum PartnershipType : byte
    {
        Sponsor = 1,
        Collaborator = 2,
        Other = 100
    }
}