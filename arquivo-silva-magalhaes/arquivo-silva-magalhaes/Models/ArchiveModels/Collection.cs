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
            this.Dimension = "0";
            this.HasDocuments = false;
            this.Visible = false;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public Enum Type { get; set; }
        public DateTime ProductionsDate { get; set; }
        public string AdministrativeHistoryAndBiography { get; set; }
        public string Provenience { get; set; }
        public bool HasDocuments { get; set; }
        public string AmbitAndContent { get; set; }
        public string Logotype { get; set; }
        public string OgranizationSystem { get; set; }
        public string Notes { get; set; }
        public string ReproductionsConditions {get; set;}
        public bool Visible { get; set;}

        [Display(ResourceType = typeof(DataStrings), Name = "CollectionDimension")]
        public string Dimension { get; set; }
        public string HistoricalDetails { get; set; }
        public System.DateTime ProductionDate { get; set; }

    }
}