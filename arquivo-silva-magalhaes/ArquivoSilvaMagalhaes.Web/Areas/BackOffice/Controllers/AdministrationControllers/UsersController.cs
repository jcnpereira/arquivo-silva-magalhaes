using ArquivoSilvaMagalhaes.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;
using ArquivoSilvaMagalhaes.Common;
using System.IO;
using ArquivoSilvaMagalhaes.Web.I18n;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.AdministrationControllers
{
    [Authorize(Roles="admins")]
    public class UsersController : BackOfficeController
    {
        public UsersController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _db = userManager;
        }

        public UserManager<ApplicationUser> _db { get; private set; }

        // GET: BackOffice/Users
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            var model = await Task.Run(() => _db.Users
                .OrderBy(u => u.UserName)
                .ToPagedList(pageNumber, 10));


            return View(model);
        }

        // GET: BackOffice/Users/Details/5
        public async Task<ActionResult> Details(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await _db.FindByNameAsync(username);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // GET: BackOffice/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/Users/Create
        [HttpPost]
        public ActionResult Create(UserRegistrationModel model)
        {
            if (_db.FindByName(model.UserName) != null)
            {
                ModelState["UserName"].Errors.Add(AuthStrings.Error_UserAlreadyExists);
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.EmailAddress,
                    RealName = model.RealName
                };

                if (model.Picture != null)
                {
                    var path = Server.MapPath("~/Public/Users/");
                    Directory.CreateDirectory(path);
                    FileUploadHelper.SaveImage(model.Picture.InputStream, 400, 400, path + model.UserName, ImageResizer.FitMode.Crop);
                    user.PictureUrl = model.UserName + ".jpg";
                }

                var result = _db.Create(user, model.Password);

                if (result.Succeeded)
                {
                    var roleName = "";
                    switch (model.Role)
                    {
                        case UserRole.Admin:
                            roleName = MembershipUtils.AdminRoleName;
                            break;
                        case UserRole.ArchiveManager:
                            roleName = MembershipUtils.ArchiveRoleName;
                            break;
                        case UserRole.ContentManager:
                            roleName = MembershipUtils.ContentRoleName;
                            break;
                        case UserRole.SiteManager:
                            roleName = MembershipUtils.PortalRoleName;
                            break;
                    }

                    _db.AddToRole(_db.FindByName(user.UserName).Id, roleName);

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        // GET: BackOffice/Users/Edit/5
        public ActionResult Edit(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = _db.FindByName(userName);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View();

            //return View(new UserRegistrationModel(user));
        }

        // POST: BackOffice/Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BackOffice/Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BackOffice/Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
