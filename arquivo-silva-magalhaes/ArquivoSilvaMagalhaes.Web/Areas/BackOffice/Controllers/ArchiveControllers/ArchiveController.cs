using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers {
   /// <summary>
   /// Base controller for all actions related to the archive.
   /// Used to restrict access to the admins and
   /// archive managers.
   /// </summary>
   [Authorize(Roles = "archivemanagers,admins,contentmanagers")]
   public class ArchiveControllerBase : BackOfficeController {
   }
}