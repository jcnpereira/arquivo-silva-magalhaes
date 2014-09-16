using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Common;
using PagedList;
using PagedList.Mvc;
using ArquivoSilvaMagalhaes.ViewModels;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class DocumentsController : Controller
    {
        
        private ITranslateableRepository<Document, DocumentTranslation> db;

        public DocumentsController()
            : this(new TranslateableGenericRepository<Document, DocumentTranslation>()) { }

        public DocumentsController(ITranslateableRepository<Document, DocumentTranslation> db)
        {
            this.db = db;
        }

        // GET: Documents
        public async Task<ActionResult> Index(int? id, int pageNumber=1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Collection col = new Collection();

            return View((await db.Entities
                .Include(doc => doc.Translations)
                .Where(doc => doc.CollectionId == id)
                .OrderBy(doc => doc.DocumentDate)
                .ToListAsync())
                .Select(doc => new TranslatedViewModel<Document, DocumentTranslation>(doc))
                .ToPagedList(pageNumber, 12));
        }

        // GET: Documents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.GetByIdAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(new TranslatedViewModel<Document, DocumentTranslation>(document));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
