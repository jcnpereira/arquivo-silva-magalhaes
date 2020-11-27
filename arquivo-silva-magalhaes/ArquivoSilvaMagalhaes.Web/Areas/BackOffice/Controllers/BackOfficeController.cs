using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers {
   /// <summary>
   /// Base controller to force authorization on all requests.
   /// </summary>
   [Authorize(Roles = "admins,archivemanagers,contentmanagers,portalmanagers")]
   public class BackOfficeController : Controller {

   }

}