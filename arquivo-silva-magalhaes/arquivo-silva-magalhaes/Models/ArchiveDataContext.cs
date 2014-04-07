using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models
{
    public class ArchiveDataContext : DbContext
    {
        public ArchiveDataContext() : base("name=ArchiveDataContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
                
        }

        public virtual DbSet<Collection> CollectionSet { get; set; }
        public virtual DbSet<CollectionText> CollectionTextSet { get; set; }

        public virtual DbSet<Document> DocumentSet { get; set; }
        public virtual DbSet<DocumentText> DocumentTextSet { get; set; }

        public virtual DbSet<Author> AuthorSet { get; set; }
        public virtual DbSet<AuthorText> AuthorTextSet { get; set; }

        public virtual DbSet<Keyword> KeywordSet { get; set; }
        public virtual DbSet<KeywordText> KeywordTextSet { get; set; }

        public virtual DbSet<Specimen> SpecimenSet { get; set; }
        public virtual DbSet<SpecimenText> SpecimenTextSet { get; set; }

        public virtual DbSet<Format> FormatSet { get; set; }

        public virtual DbSet<Process> ProcessSet { get; set; }
        public virtual DbSet<ProcessText> ProcessTextSet { get; set; }

        public virtual DbSet<Classification> ClassificationSet { get; set; }
        public virtual DbSet<ClassificationText> ClassificationTextSet { get; set; }

        public virtual DbSet<DigitalPhotograph> DigitalPhotographSet { get; set; }

        public virtual DbSet<ShowcasePhoto> ShowcasePhotoSet { get; set; }
        public virtual DbSet<ShowcasePhotoText> ShowcasePhotoTextSet { get; set; }


        // Site-related tables.

        /// <summary>
        /// Events of this archive.
        /// </summary>
        public virtual DbSet<Event> EventSet { get; set; }
        public virtual DbSet<EventText> EventTextSet { get; set; }

        public virtual DbSet<NewsItem> NewsSet { get; set; }
        public virtual DbSet<NewsText> NewsTextSet { get; set; }

        public virtual DbSet<BannerPhotograph> BannerPhotographSet { get; set; }
        public virtual DbSet<BannerPhotographText> BannerPhotographTextSet { get; set; }

        public virtual DbSet<Collaborator> CollaboratorSet { get; set; }

        public virtual DbSet<DocumentAttachment> DocumentAttachmentSet { get; set; }

        public virtual DbSet<Partnership> PartnershipSet { get; set; }
        
        public virtual DbSet<ReferencedLink> ReferencedLinkSet { get; set; }
        
        public virtual DbSet<SpotlightVideo> SpotlightVideoSet { get; set; }

        public virtual DbSet<TechnicalDocument> TecnhicalDocumentSet { get; set; }

        public System.Data.Entity.DbSet<ArquivoSilvaMagalhaes.Models.SiteModels.Archive> Archives { get; set; }

    }
}