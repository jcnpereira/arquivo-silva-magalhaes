using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using ArquivoSilvaMagalhaes.Web.I18n;
using ArquivoSilvaMagalhaes.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels
{
    public class ImageViewModel
    {
        public ImageViewModel(Image i)
        {
            this.Image = i;

            Keywords = Image.Keywords.ToList().Select(k => new TranslatedViewModel<Keyword, KeywordTranslation>(k))
                            .ToList();

            Classification = new TranslatedViewModel<Classification, ClassificationTranslation>(Image.Classification);

            Specimens = Image.Specimens.ToList().Select(s => new TranslatedViewModel<Specimen, SpecimenTranslation>(s))
                             .ToList();
        }

        public Image Image { get; set; }

        public IEnumerable<TranslatedViewModel<Keyword, KeywordTranslation>> Keywords { get; set; }
        public TranslatedViewModel<Classification, ClassificationTranslation> Classification { get; set; }
        public IEnumerable<TranslatedViewModel<Specimen, SpecimenTranslation>> Specimens { get; set; }
    }

    public class ImageEditViewModel
    {
        public ImageEditViewModel()
            : this(new Image())
        {
        }

        public ImageEditViewModel(Image i)
        {
            this.Image = i;

            AvailableKeywords = new List<SelectListItem>();

            AvailableDocuments = new List<SelectListItem>
            {
                new SelectListItem { Selected = true, Text = "" }
            };

            AvailableClassifications = new List<SelectListItem>
            {
                new SelectListItem { Selected = true, Text = "" }
            };

            KeywordIds = i.Keywords.Select(k => k.Id).ToArray();
        }

        public void PopulateDropDownLists(
            IEnumerable<Document> documents,
            IEnumerable<Classification> classifications,
            IEnumerable<Keyword> keywords)
        {
            AvailableDocuments.AddRange(documents
                .OrderBy(d => d.Id)
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.CatalogCode + " - " + d.Title,
                    Selected = d.Id == Image.DocumentId
                }));

            AvailableKeywords.AddRange(keywords
                .OrderBy(k => k.Id)
                .ToList()
                .Select(k => new TranslatedViewModel<Keyword, KeywordTranslation>(k))
                .Select(k => new SelectListItem
                {
                    Value = k.Entity.Id.ToString(),
                    Text = k.Translation.Value,
                    Selected = KeywordIds.Contains(k.Entity.Id)
                }));

            AvailableClassifications.AddRange(classifications
                .OrderBy(c => c.Id)
                .ToList()
                .Select(c => new TranslatedViewModel<Classification, ClassificationTranslation>(c))
                .Select(c => new SelectListItem
                {
                    Value = c.Entity.Id.ToString(),
                    Text = c.Translation.Value,
                    Selected = Image.ClassificationId == c.Entity.Id
                }));
        }

        public Image Image { get; set; }

        [Display(ResourceType = typeof(ImageStrings), Name = "Keywords")]
        public List<SelectListItem> AvailableKeywords { get; set; }

        [Display(ResourceType = typeof(ImageStrings), Name = "Document")]
        public List<SelectListItem> AvailableDocuments { get; set; }

        [Display(ResourceType = typeof(ImageStrings), Name = "Classification")]
        public List<SelectListItem> AvailableClassifications { get; set; }

        [Required(ErrorMessageResourceType = typeof(LayoutStrings), ErrorMessageResourceName = "MustChooseAtLeastOne")]
        public int[] KeywordIds { get; set; }

        [Display(ResourceType = typeof(ImageStrings), Name = "ImageUrl")]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}