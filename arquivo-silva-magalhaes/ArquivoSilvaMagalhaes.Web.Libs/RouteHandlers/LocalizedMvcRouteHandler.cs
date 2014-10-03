using ArquivoSilvaMagalhaes.Common;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;
using System;

namespace ArquivoSilvaMagalhaes.Web.Libs.RouteHandlers
{
    public class LocalizedRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            // Get the language from the url -- most prioritary.
            var urlLocale = requestContext.RouteData.Values["culture"] as string ?? "";

            var culturesToTest = new List<string>();

            if (!string.IsNullOrEmpty(urlLocale))
            {
                culturesToTest.Add(urlLocale);
            }


            // Get the language from the cookie, if it exists -- 2nd priority.
            var cookieLocale = requestContext.HttpContext.Request.Cookies["locale"];

            if (cookieLocale != null)
            {
                culturesToTest.Add(cookieLocale.Value);
            }

            // Add all the user's requested languages from the Accept-Language header -- 3rd priority.
            culturesToTest.AddRange(requestContext
                .HttpContext
                .Request
                .UserLanguages
                .Select(l => l.Split(';')[0])
                .ToList());

            var chosenLanguage = LanguageDefinitions.GetClosestLanguageCode(culturesToTest.ToArray());
            var localeCookie = new HttpCookie("locale", chosenLanguage);
            localeCookie.Expires = DateTime.Now.AddYears(2);

            requestContext.HttpContext.Response.SetCookie(localeCookie);

            // Redirect if the url locale doesn't match the culture name.
            if (!LanguageDefinitions.AreCodesEquivalent(chosenLanguage, urlLocale))
            {
                var routeValues = requestContext.RouteData.Values;
                routeValues["culture"] =
                    LanguageDefinitions.GetClosestLanguageCode(chosenLanguage);

                return new RedirectHandler(new UrlHelper(requestContext).RouteUrl(routeValues));
            }

            try
            {
                // Set the thread's language.
                var culture = CultureInfo.GetCultureInfo(chosenLanguage);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            catch (CultureNotFoundException)
            {
                // if CultureInfo.GetCultureInfo throws exception
                // we should redirect with default locale
                return GetDefaultLocaleRedirectHandler(requestContext);
            }

            return base.GetHttpHandler(requestContext);
        }

        /// <summary>
        /// Gets a handler that redirects the request
        /// to an url that has the default language for the
        /// application.
        /// </summary>
        private static IHttpHandler GetDefaultLocaleRedirectHandler(RequestContext requestContext)
        {
            var routeValues = requestContext.RouteData.Values;
            routeValues["culture"] = 
                LanguageDefinitions.GetClosestLanguageCode(requestContext.HttpContext.Request.UserLanguages);

            return new RedirectHandler(new UrlHelper(requestContext).RouteUrl(routeValues));
        }
    }
}
