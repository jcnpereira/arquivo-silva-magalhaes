using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class Collection
    {
        public Collection()
        {
            this.CollectionTexts = new HashSet<CollectionText>();
            this.Documents = new HashSet<Document>();
            this.Authors = new HashSet<Author>();
        }

        [Key]
        public int Id { get; set; }
        public CollectionType Type { get; set; }
        public System.DateTime ProductionDate { get; set; }
        public string LogoLocation { get; set; }
        public bool HasAttachments { get; set; }
        public string OrganizationSystem { get; set; }
        public string Notes { get; set; }
        public bool IsVisible { get; set; }
        public string CatalogCode { get; set; }

        public virtual ICollection<CollectionText> CollectionTexts { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
    }

    public enum CollectionType : byte
    {
        Regular = 1,
        Background = 2,
        Other = 3
    }

    public partial class CollectionText
    {
        public CollectionText()
        {
            this.LanguageCode = "pt";
        }

        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Provenience { get; set; }
        public string AdministrativeAndBiographicStory { get; set; }
        public string Dimension { get; set; }
        public string FieldAndContents { get; set; }
        public string CopyrightInfo { get; set; }
        public int CollectionId { get; set; }

        public virtual Collection Collection { get; set; }
    }
}