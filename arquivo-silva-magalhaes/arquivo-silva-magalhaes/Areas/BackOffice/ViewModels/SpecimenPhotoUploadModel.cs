using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class SpecimenPhotoUploadModel
    {

        public int Id { get; set; }

        [Required]
        public int SpecimenId { get; set; }

        public List<PhotoUploadModelItem> Items { get; set; }

        public List<HttpPostedFileBase> Photos { get; set; }
    }

    public class PhotoUploadModelItem : IValidatableObject
    {
        [Required]
        [Range(1, 31)]
        public int ScanDay { get; set; }

        [Required]
        [Range(1, 12)]
        public int ScanMonth { get; set; }

        [Required]
        public int ScanYear { get; set; }

        [Required]
        public string Process { get; set; }

        public string OriginalFileName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string CopyrightInfo { get; set; }

        public bool IsVisible { get; set; }

        public bool IsToConsider { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            // Check for a valid date.
            try
            {
                var d = new DateTime(ScanYear, ScanMonth, ScanDay);
            }
            catch (Exception)
            {
                errors.Add(new ValidationResult(ErrorStrings.InvalidDate));
            }

            return errors;
        }
    }
}