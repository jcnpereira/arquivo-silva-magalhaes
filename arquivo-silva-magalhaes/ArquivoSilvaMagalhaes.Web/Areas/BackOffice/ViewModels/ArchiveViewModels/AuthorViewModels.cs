using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels
{

    /// <summary>
    /// To be used in Create and Edit views.
    /// </summary>
    public class AuthorEditViewModel
    {
        public Author Author { get; set; }

        [Display(ResourceType = typeof(AuthorStrings), Name = "PictureFileName")]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}