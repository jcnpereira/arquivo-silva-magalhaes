using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.ViewModels;
using System.Threading.Tasks;
using PagedList;
using PagedList.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class AuthorsController : Controller
    {
        //private ArchiveDataContext db = new ArchiveDataContext();

        // GET: Authors
        //public ActionResult Index()
        //{
        //    return View(GetAuthorsViewModel());
        //}

        //private List<AuthorsViewModel> GetAuthorsViewModel()
        //{
        //    List<AuthorsViewModel> authorView = new List<AuthorsViewModel>();
        //    foreach (var item in db.Authors.ToList())
        //    {
        //        AuthorsViewModel a = new AuthorsViewModel();
        //        a.Id = item.Id;
        //        a.FirstName = item.FirstName;
        //        a.LastName = item.LastName;
        //        a.BirthDate = item.BirthDate;
        //        a.DeathDate = item.DeathDate.Value;
        //        a.Nationality = item.Translations.LastOrDefault().Nationality;
        //        a.Biagraphy = item.Translations.LastOrDefault().Biography;
        //        a.Curriculum = item.Translations.LastOrDefault().Curriculum;
                
        //        authorView.Add(a);
        //    }
        //    return authorView;
        //}

        private ITranslateableRepository<Author, AuthorTranslation> db;

        public AuthorsController()
            : this(new TranslateableGenericRepository<Author, AuthorTranslation>()) { }

        public AuthorsController(ITranslateableRepository<Author, AuthorTranslation> db)
        {
            this.db = db;
        }


        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View((await db.Entities
                            //.Include(col => col.Authors)
                            //.Where(col => col.IsVisible)
                            .OrderByDescending(a => a.BirthDate)
                            .ToListAsync())
                            .Select(a => new TranslatedViewModel<Author, AuthorTranslation>(a))
                            .ToPagedList(pageNumber, 12));
        }


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = await db.GetByIdAsync(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(new TranslatedViewModel<Author, AuthorTranslation>(author));
        }

        // GET: Authors/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Author author = db.Authors.Find(id);
        //    if (author == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(author);
        //}

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
       
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
