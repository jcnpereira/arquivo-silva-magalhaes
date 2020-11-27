using ArquivoSilvaMagalhaes.Models.Translations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public class Image
    {
        public Image()
        {
            Keywords = new List<Keyword>();
            Specimens = new List<Specimen>();
            Translations = new List<ImageTranslation>();
            ShowcasePhotos = new List<ShowcasePhoto>();
        }

        public int Id { get; set; }

        //[Display(ResourceType = typeof(ImageStrings), Name = "ProductionDate")]
        //[DataType(DataType.Date)]
        //public DateTime? ProductionDate { get; set; }

        [Display(ResourceType = typeof(ImageStrings), Name = "ProductionDate")]
        public string ProductionDate { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        [Display(ResourceType = typeof(ImageStrings), Name = "ShowCoordinates")]
        public bool ShowCoordinates { get; set; }

       [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [Index(IsUnique = true)]
        [StringLength(100)]
        [RegularExpression("^[A-Za-z0-9]+-[A-Za-z0-9]+-[A-Za-z0-9]+$", ErrorMessageResourceType = typeof(ImageStrings), ErrorMessageResourceName = "CodeFormat")]
        [Display(ResourceType = typeof(ImageStrings), Name = "ImageCode")]
        public string ImageCode { get; set; }

        [Display(ResourceType = typeof(ImageStrings), Name = "ImageUrl")]
        public string ImageUrl { get; set; }

        [Display(ResourceType = typeof(ImageStrings), Name = "IsVisible")]
        public bool IsVisible { get; set; }

        [Display(ResourceType = typeof(ImageStrings), Name = "ShowImage")]
        public bool ShowImage { get; set; }

        [ForeignKey("DocumentId")]
        [Display(ResourceType = typeof(ImageStrings), Name = "Document")]
        public virtual Document Document { get; set; }

        [Display(ResourceType = typeof(ImageStrings), Name = "Document")]
        public int DocumentId { get; set; }

        public virtual IList<ImageTranslation> Translations { get; set; }

        [Display(ResourceType = typeof(ImageStrings), Name = "Keywords")]
        public virtual IList<Keyword> Keywords { get; set; }

        public virtual IList<Specimen> Specimens { get; set; }

        public virtual IList<ShowcasePhoto> ShowcasePhotos { get; set; }

        [ForeignKey("ClassificationId")]
        [Display(ResourceType = typeof(ImageStrings), Name = "Classification")]
        public virtual Classification Classification { get; set; }

        [Display(ResourceType = typeof(ImageStrings), Name = "Classification")]
        public int ClassificationId { get; set; }

    }

    public class ImageTranslation : EntityTranslation
    {
        [Key, Column(Order = 0)]
        public int ImageId { get; set; }
        [Key, Column(Order = 1)]
        public override string LanguageCode { get; set; }

       [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [StringLength(100)]
        [Display(ResourceType = typeof(ImageStrings), Name = "Title")]
        public string Title { get; set; }

       [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [StringLength(500)]
        [Display(ResourceType = typeof(ImageStrings), Name = "Subject")]
        public string Subject { get; set; }

        [StringLength(500)]
        [Display(ResourceType = typeof(ImageStrings), Name = "Publication")]
        public string Publication { get; set; }

       [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [StringLength(80)]
        [Display(ResourceType = typeof(ImageStrings), Name = "Location")]
        public string Location { get; set; }

       [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [StringLength(500)]
        [Display(ResourceType = typeof(ImageStrings), Name = "Description")]
        public string Description { get; set; }

        [ForeignKey("ImageId")]
        public Image Image { get; set; }
    }
}