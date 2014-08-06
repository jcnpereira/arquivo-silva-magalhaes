using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using System.Data.Entity;

namespace ArquivoSilvaMagalhaes.Models
{
    public class ArchiveDataContext : DbContext
    {
        public ArchiveDataContext() : base("name=ArchiveDataContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Image>()
            //    .HasMany(i => i.Keywords)
            //    .WithRequired(i => i.)


            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<CollectionTranslation> CollectionTranslations { get; set; }

        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentTranslation> DocumentTranslations { get; set; }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<AuthorTranslation> AuthorTranslations { get; set; }

        public virtual DbSet<Keyword> Keywords { get; set; }
        public virtual DbSet<KeywordTranslation> KeywordTranslations { get; set; }

        public virtual DbSet<Specimen> Specimens { get; set; }
        public virtual DbSet<SpecimenTranslation> SpecimenTranslations { get; set; }

        public virtual DbSet<Format> Formats { get; set; }

        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<ProcessTranslation> ProcessTranslations { get; set; }

        public virtual DbSet<Classification> Classifications { get; set; }
        public virtual DbSet<ClassificationTranslation> ClassificationTranslations { get; set; }

        public virtual DbSet<DigitalPhotograph> DigitalPhotographs { get; set; }

        public virtual DbSet<ShowcasePhoto> ShowcasePhotoes { get; set; }
        public virtual DbSet<ShowcasePhotoTranslation> ShowcasePhotoTranslations { get; set; }

        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<ImageTranslation> ImageTranslations { get; set; }

        // Site-related tables.

        /// <summary>
        /// Events of this archive.
        /// </summary>
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventTranslation> EventTranslations { get; set; }

        public virtual DbSet<NewsItem> NewsItems { get; set; }
        public virtual DbSet<NewsItemTranslation> NewsItemTranslations { get; set; }

        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<BannerTranslation> BannerTranslations { get; set; }

        public virtual DbSet<Collaborator> Collaborators { get; set; }

        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<AttachmentTranslation> AttachmentTranslations { get; set; }

        public virtual DbSet<Partnership> Partnerships { get; set; }

        public virtual DbSet<ReferencedLink> ReferencedLinks { get; set; }

        public virtual DbSet<SpotlightVideo> SpotlightVideos { get; set; }

        public virtual DbSet<TechnicalDocument> TechnicalDocuments { get; set; }

        public virtual DbSet<Archive> Archives { get; set; }
        public virtual DbSet<ArchiveTranslations> ArchiveTranslations { get; set; }

        public virtual DbSet<Contact> ArchiveContacts { get; set; }
    }
}