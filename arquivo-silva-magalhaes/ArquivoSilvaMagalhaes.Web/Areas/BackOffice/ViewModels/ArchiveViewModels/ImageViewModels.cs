using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels
{


    public class ImageViewModel
    {
        public Image Image { get; set; }

        public List<SelectListItem> AvailableKeywords { get; set; }
        public List<SelectListItem> AvailableDocuments { get; set; }
    }

    public class ImageEditViewModel
    {
        public ImageEditViewModel() : this(new Image(), true)
        {
        }

        public ImageEditViewModel(Image i, bool addWatermark)
        {
            this.Image = i;
            this.AddWatermark = addWatermark;

            AvailableKeywords = new List<SelectListItem>();

            AvailableDocuments = new List<SelectListItem>
            {
                new SelectListItem { Selected = true, Text = UiPrompts.ChooseOne }
            };

            AvailableClassifications = new List<SelectListItem>
            {
                new SelectListItem { Selected = true, Text = UiPrompts.ChooseOne }
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

        [Required(ErrorMessageResourceType = typeof(ValidationErrorStrings), ErrorMessageResourceName = "MustChooseAtLeastOne")]
        public int[] KeywordIds { get; set; }

        public HttpPostedFileBase ImageUpload { get; set; }

        public bool AddWatermark { get; set; }
    }
}