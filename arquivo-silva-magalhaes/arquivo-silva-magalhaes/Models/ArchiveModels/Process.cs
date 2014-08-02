﻿using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Resources.ModelTranslations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public class Process
    {
        public Process()
        {
            this.Translations = new List<ProcessTranslation>();
            this.Specimens = new List<Specimen>();
        }

        [Key]
        public int Id { get; set; }

        public virtual IList<ProcessTranslation> Translations { get; set; }
        public virtual IList<Specimen> Specimens { get; set; }
    }

    public class ProcessTranslation
    {
        [Key, Column(Order = 0)]
        public int ProcessId { get; set; }

        [Key, Column(Order = 1), Required]
        public string LanguageCode { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        [Display(ResourceType = typeof(ProcessStrings), Name = "Value")]
        public string Value { get; set; }

        [ForeignKey("ProcessId")]
        public virtual Process Process { get; set; }
    }
}