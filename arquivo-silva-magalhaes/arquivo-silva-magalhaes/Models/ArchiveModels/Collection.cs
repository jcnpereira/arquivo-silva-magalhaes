using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class Collection
    {
        public Collection()
        {
            this.Dimension = 0;
        }

        public int Id { get; set; }
        public string Provenience { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "CollectionDimension")]
        public short Dimension { get; set; }
        public string HistoricalDetails { get; set; }
        public string Type { get; set; }
        public System.DateTime ProductionDate { get; set; }
    }
}