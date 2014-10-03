using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Web.I18n;
using ArquivoSilvaMagalhaes.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels
{
    public class DocumentEditViewModel
    {
        public DocumentEditViewModel()
            : this(new Document())
        {
        }

        public DocumentEditViewModel(Document d)
        {
            this.Document = d;

            AvailableAuthors = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Value = "",
                        Text = UiPrompts.ChooseOne
                    }
                };

            AvailableCollections = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Value = "",
                        Text = UiPrompts.ChooseOne
                    }
                };
        }

        public async Task PopulateDropDownLists(IQueryable<Author> authors, IQueryable<Collection> collections)
        {
            (AvailableAuthors as List<SelectListItem>).AddRange(await authors.Select(a => new SelectListItem
            {
                Text = a.LastName + ", " + a.FirstName,
                Value = a.Id.ToString(),
                Selected = Document.AuthorId == a.Id
            }).ToListAsync());

            (AvailableCollections as List<SelectListItem>).AddRange((await collections
                .ToListAsync())
                .Select(c => new TranslatedViewModel<Collection, CollectionTranslation>(c))
                .Select(c => new SelectListItem
                {
                    Selected = c.Entity.Id == Document.CollectionId,
                    Value = c.Entity.Id.ToString(),
                    Text = c.Entity.CatalogCode + " - " + "'" + c.Translation.Title + "'"
                }));
        }

        public Document Document { get; set; }

        public IList<SelectListItem> AvailableAuthors { get; set; }

        public IList<SelectListItem> AvailableCollections { get; set; }
    }
}