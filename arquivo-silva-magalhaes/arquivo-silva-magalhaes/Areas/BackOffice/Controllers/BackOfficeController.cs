using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    /// <summary>
    /// Controller-base para forçar a autenticação de todas
    /// as requests ao back-office.
    /// </summary>
    [Authorize]
    public class BackOfficeController : Controller
    {
        // Sem Actions.
    }
}