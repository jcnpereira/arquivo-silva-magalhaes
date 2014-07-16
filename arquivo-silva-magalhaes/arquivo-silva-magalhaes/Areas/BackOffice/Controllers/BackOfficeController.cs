using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    /// <summary>
    /// Base controller to force authorization on all requests.
    /// </summary>
    [Authorize]
    public class BackOfficeController : Controller
    {
        // No actions.
    }
}