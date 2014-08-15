using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;
using ArquivoSilvaMagalhaes.Models;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class EventsViewController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /EventsView/
        public async Task<ActionResult> Index()
        {
            return View(await db.Events.ToListAsync());
        }

        // GET: /EventsView/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EventEditViewModel eventeditviewmodel = await db.EventEditViewModels.FindAsync(id);
        //    if (eventeditviewmodel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(eventeditviewmodel);
        //}

    }
}
