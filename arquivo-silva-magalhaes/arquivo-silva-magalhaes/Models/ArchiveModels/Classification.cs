using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    /// <summary>
    /// Defines a classification of a photographic specimen.
    /// </summary>
    public partial class Classification
    {
        public Classification()
        {
            this.ClassificationTexts = new HashSet<ClassificationText>();
            this.Specimens = new HashSet<Specimen>();
        }

        [Key]
        public int Id { get; set; }

        public virtual ICollection<ClassificationText> ClassificationTexts { get; set; }
        public virtual ICollection<Specimen> Specimens { get; set; }
    }

    /// <summary>
    /// Details about a classification.
    /// </summary>
    public partial class ClassificationText
    {

        [Key, Column(Order = 0)]
        public int ClassificationId { get; set; }

        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }

        /// <summary>
        /// The classification details.
        /// </summary>
        public string Value { get; set; }

    }
}