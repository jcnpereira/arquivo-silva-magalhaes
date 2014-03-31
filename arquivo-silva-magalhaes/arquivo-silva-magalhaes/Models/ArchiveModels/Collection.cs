using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    /// <summary>
    /// Defines a collection.
    /// </summary>
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

        /// <summary>
        /// The type of this collection.
        /// </summary>
        public CollectionType Type { get; set; }

        /// <summary>
        /// The date on which this collection was created.
        /// </summary>
        public DateTime ProductionDate { get; set; }

        /// <summary>
        /// Location of the logo of this collection.
        /// </summary>
        public string LogoLocation { get; set; }

        public bool HasAttachments { get; set; }

        public string OrganizationCode { get; set; }

        public string Notes { get; set; }
        public bool IsVisible { get; set; }

        /// <summary>
        /// Code used by the archive to physically catalog this
        /// collection.
        /// </summary>
        public string CatalogCode { get; set; }

        public virtual ICollection<CollectionText> CollectionTexts { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Author> Authors { get; set; }

        #region Non-mapped Attributes

        [NotMapped]
        public string Title
        {
            get
            {
                return GetTextForCurrentLanguageOrDefault().Title;
            }
        }

        [NotMapped]
        public string Description
        {
            get
            {
                return GetTextForCurrentLanguageOrDefault().Description;
            }
        }

        [NotMapped]
        public string Provenience
        {
            get
            {
                return GetTextForCurrentLanguageOrDefault().Provenience;
            }
        }

        [NotMapped]
        public string AdministrativeAndBiographicStory
        {
            get
            {
                return GetTextForCurrentLanguageOrDefault().AdministrativeAndBiographicStory;
            }
        }

        [NotMapped]
        public string Dimension
        {
            get
            {
                return GetTextForCurrentLanguageOrDefault().Dimension;
            }
        }

        [NotMapped]
        public string FieldAndContents
        {
            get
            {
                return GetTextForCurrentLanguageOrDefault().FieldAndContents;
            }
        }

        [NotMapped]
        public string CopyrightInfo
        {
            get
            {
                return GetTextForCurrentLanguageOrDefault().CopyrightInfo;
            }
        }

        #endregion

        /// <summary>
        /// Gets the most appropriate translation for the current ui language.
        /// </summary>
        private CollectionText GetTextForCurrentLanguageOrDefault()
        {
            var locale = Thread.CurrentThread.CurrentUICulture.ToString();
            return CollectionTexts
                .First(t => locale.Contains(t.LanguageCode) || t.LanguageCode.Contains(CollectionText.DefaultLanguageCode));
        }
    }

    public enum CollectionType : byte
    {
        Collection = 1,
        Fond = 2,
        Other = 100
    }

    public partial class CollectionText
    {
        public const string DefaultLanguageCode = "pt";

        public CollectionText()
        {
            this.LanguageCode = DefaultLanguageCode;
        }

        [Key, Column(Order = 0)]
        public int Id { get; set; }

        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Provenience { get; set; }
        public string AdministrativeAndBiographicStory { get; set; }
        public string Dimension { get; set; }
        public string FieldAndContents { get; set; }
        public string CopyrightInfo { get; set; }

        [Required]
        public virtual Collection Collection { get; set; }
    }
}