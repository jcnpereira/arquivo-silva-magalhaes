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
    [Authorize(Roles = "admins")]
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
            using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext())))
            {
                var roles = roleManager.Roles.ToList().Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = AuthStrings.ResourceManager.GetString(r.Name)
                });

                return View(new UserRegistrationModel
                    {
                        AvailableRoles = roles
                    });
            }
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
                    foreach (var r in model.Roles)
                    {
                        _db.AddToRole(user.Id, r);
                    }

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        // GET: BackOffice/Users/Edit/5
        public ActionResult ChangePassword(string userName)
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

            return View(new UserChangePasswordModel
                {
                    UserName = user.UserName
                });
        }

        // POST: BackOffice/Users/Edit/5
        [HttpPost]
        public ActionResult ChangePassword(UserChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _db.FindByName(model.UserName);

                _db.RemovePassword(user.Id);
                var result = _db.AddPassword(user.Id, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, AuthStrings.UnableToChangePassword);

            return View(model);
        }

        public ActionResult ChangeRole(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (userName.ToLower() == "admin")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, AuthStrings.CannotChangeRoleOfAdmin);
            }

            var user = _db.FindByName(userName);

            if (user == null)
            {
                return HttpNotFound();
            }

            using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext())))
            {
                var roles = roleManager.Roles.ToList().Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = AuthStrings.ResourceManager.GetString(r.Name),
                    Selected = _db.IsInRole(user.Id, r.Name)
                });

                return View(new UserChangeRoleModel
                {
                    UserName = user.UserName,
                    AvailableRoles = roles
                });
            }
        }

        // POST: BackOffice/Users/Edit/5
        [HttpPost]
        public ActionResult ChangeRole(UserChangeRoleModel model)
        {
            if (model.UserName.ToLower() == "admin")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, AuthStrings.CannotChangeRoleOfAdmin);
            }

            if (ModelState.IsValid)
            {
                var user = _db.FindByName(model.UserName);

                foreach (var role in _db.GetRoles(user.Id))
                {
                    _db.RemoveFromRole(user.Id, role);
                }

                var results = model.Roles.Select(r => _db.AddToRole(user.Id, r));

                if (results.All(r => r.Succeeded))
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, AuthStrings.UnableToChangeRoles);
            }

            return View(model);
        }

        // GET: BackOffice/Users/Delete/5
        public ActionResult Delete(string userName)
        {
            if (userName.ToLower() == "admin")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, AuthStrings.CannotChangeRoleOfAdmin);
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = _db.FindByName(userName);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: BackOffice/Users/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(string userName)
        {
            if (userName.ToLower() == "admin")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, AuthStrings.CannotChangeRoleOfAdmin);
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = _db.FindByName(userName);

            if (user == null)
            {
                return HttpNotFound();
            }

            var result = _db.Delete(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", AuthStrings.CannotDeleteUser);

            return View(user);
        }
    }
}
