using System.Web.Optimization;

namespace ArquivoSilvaMagalhaes
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-date-picker").Include(
                //"~/Scripts/bootstrap-datepicker.js"));
                        "~/Scripts/bs-datepicker/moment.js",
                        "~/Scripts/bs-datepicker/bootstrap-datetimepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js"));



            bundles.Add(new ScriptBundle("~/Scripts/backoffice/extras").Include(
                       "~/Scripts/BackOffice/modal-dialog.js",
                       "~/Scripts/BackOffice/code-suggestions.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Styles/Bootstrap/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/customcss").Include(
                      "~/Content/Styles/Site.css"));

            bundles.Add(new StyleBundle("~/Content/datepicker-bootstrap").Include(
                        "~/Content/Styles/DatePicker/bootstrap-datetimepicker.css"));

            RegisterFrontOfficeBundles(bundles);
        }

        private static void RegisterFrontOfficeBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/FrontOffice/Styles").Include(
                "~/Content/Styles/frontoffice/*.css"));

            bundles.Add(new ScriptBundle("~/Scripts/frontoffice").Include(
                "~/Scripts/frontoffice/*.js"));
        }
    }
}
