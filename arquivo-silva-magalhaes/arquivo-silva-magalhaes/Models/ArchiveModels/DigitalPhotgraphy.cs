using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class DigitalPhotgraphy
    {
        DigitalPhotgraphy()
        {
            this.Title = "Sem título";
            this.Discription = " Sem descrição";
            this.visible = false;
        }
        public string Title {get; set;}     
        public string Discription {get; set;}
        public System.DateTime DigitalizationDate { get; set; }
        public string Localization {get; set;}
        public string DigitalizationProcess { get; set; }
        public string ReproductionsConditions {get; set;}
        public bool visible {get; set;}

        public virtual PhotographicSpecimen PhotogtaphicSpecimen;

    }
}