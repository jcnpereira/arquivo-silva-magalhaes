using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public class ArchiveDataContext : DbContext
    {
        public ArchiveDataContext() : base("name=ArchiveDataContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Convert datetime column types to datetime2, which allow a broader range of values.
            modelBuilder.Entity<Document>()
                    .Property(d => d.DocumentDate)
                    .HasColumnType("datetime2");

            modelBuilder.Entity<Document>()
                    .Property(d => d.CatalogDate)
                    .HasColumnType("datetime2");

            modelBuilder.Entity<Collection>()
                    .Property(c => c.ProductionDate)
                    .HasColumnType("datetime2");

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<PhotographicSpecimen> PhotographicSpecies { get; set; }
        public virtual DbSet<PhotographicProcess> PhotographicProcesses { get; set; }
        public virtual DbSet<PhotographicFormat> PhotographicFormats { get; set; }
        public virtual DbSet<KeyWord> KeyWords { get; set; }
        public virtual DbSet<DigitalPhoto> DigitalPhotos { get; set; }
    }
}