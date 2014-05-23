using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public enum DocumentType : byte
    {
        Imagem = 1,
        Texto = 2,
        Video = 3,
        Outro = 100
    }


    public class TechnicalDocument
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title;
        [Required]
        public string UriPath;
        [Required]
       // [DataType(DataType.Date),DisplayFormat( DataFormatString="{0:dd/MM/yy}", ApplyFormatInEditMode=true )]
        [DataType(DataType.Date)] 
        public DateTime UploadedDate;
        [Required]
       // [DataType(DataType.Date),DisplayFormat( DataFormatString="{0:dd/MM/yy}", ApplyFormatInEditMode=true )]
        [DataType(DataType.Date)]
        public DateTime LastModificationDate;
        [Required]
        public String Format;
        [Required]
        public DocumentType DocumentType;
        [Required]
        public string LanguageCode;

    }
}