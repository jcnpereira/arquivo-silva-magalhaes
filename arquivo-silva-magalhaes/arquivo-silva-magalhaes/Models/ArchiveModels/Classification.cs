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
    public class Classification
    {
        public Classification()
        {
            this.Translations = new HashSet<ClassificationTranslation>();
            this.Specimens = new HashSet<Specimen>();
        }

        [Key]
        public int Id { get; set; }

        public virtual HashSet<ClassificationTranslation> Translations { get; set; }
        public virtual HashSet<Specimen> Specimens { get; set; }
    }

    /// <summary>
    /// Details about a classification.
    /// </summary>
    public class ClassificationTranslation
    {

        [Key, Column(Order = 0)]
        public int ClassificationId { get; set; }

        [Key, Column(Order = 1), Required]
        public string LanguageCode { get; set; }

        /// <summary>
        /// The classification details.
        /// </summary>
        [Required, Display(ResourceType = typeof(DataStrings), Name = "Classification")]
        public string Value { get; set; }

        [ForeignKey("ClassificationId")]
        public virtual Classification Classification { get; set; }
    }
}