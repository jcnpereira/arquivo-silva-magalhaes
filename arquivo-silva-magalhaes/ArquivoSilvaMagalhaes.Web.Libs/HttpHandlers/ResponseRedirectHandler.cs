using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ArquivoSilvaMagalhaes.Web.Libs
{
    /// <summary>
    /// Http handler that causes the response
    /// to be redirected to a different url.
    /// </summary>
    class RedirectHandler : IHttpHandler
    {
        private string _newUrl;

        public RedirectHandler(string newUrl)
        {
            this._newUrl = newUrl;
        }

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Redirect(this._newUrl);
        }
    }
}
