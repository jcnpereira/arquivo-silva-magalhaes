﻿using ArquivoSilvaMagalhaes.Models.Translations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public class Format
    {
        public Format()
        {
            this.Specimens = new HashSet<Specimen>();
        }

        [Key]
        public int Id { get; set; }

       [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        [Display(ResourceType = typeof(FormatStrings), Name = "Format")]
        public string FormatDescription { get; set; }

        public virtual ICollection<Specimen> Specimens { get; set; }
    }
}