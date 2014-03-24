using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class SpotLightPhotography
    {

        SpotLightPhotography()
        {
            this.Comment = "";
            this.Visible = false;
        }

        public string Comment { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Visible { get; set; }
        public DateTime StartVisualizationDate { get; set; }
        public DateTime EndVisualizationDate { get; set; }

        public virtual DigitalPhotgraphy DigitalPhotography { get; set; }
    }
}