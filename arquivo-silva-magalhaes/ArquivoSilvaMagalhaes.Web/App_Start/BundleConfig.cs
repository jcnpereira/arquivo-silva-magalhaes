using System.Web.Optimization;

namespace ArquivoSilvaMagalhaes
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyleBundles(bundles);
            RegisterScriptBundles(bundles);

            BundleTable.EnableOptimizations = true;
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            // Style bundle that properly stylizes the datepicker.
            bundles.Add(new StyleBundle("~/bundles/css/vendor/bs-datetimepicker").Include(
                "~/Content/Styles/DatePicker/bootstrap-datetimepicker.css"
            ));

            bundles.Add(new StyleBundle("~/bundles/css/backoffice-main").Include(
                "~/Content/Styles/Bootstrap/bootstrap.css",
                "~/Content/Styles/Site.css"
            ));

            bundles.Add(new StyleBundle("~/bundles/css/frontoffice-main").Include(
                "~/Content/Styles/frontoffice/main.css"
            ));

            bundles.Add(new StyleBundle("~/bundles/css/video-overlay").Include(
                "~/Content/Styles/frontoffice/video-overlay.css"
            ));

            bundles.Add(new StyleBundle("~/bundles/css/animations").Include(
                "~/Content/Styles/frontoffice/afsm-animations.css"
            ));
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            // Common bundle that includes jQuery, Respond.js and Bootstrap.
            bundles.Add(new ScriptBundle("~/bundles/js/common").Include(
                "~/Scripts/vendor/jquery/jquery-{version}.js",
                "~/Scripts/vendor/bootstrap/bootstrap.js",
                "~/Scripts/vendor/respond/respond.js"
            ));

            // Bundle for unobtrusive validation purposes.
            bundles.Add(new ScriptBundle("~/bundles/js/val").Include(
                "~/Scripts/vendor/jquery/jquery.validate*"
            ));

            // Bundle that contains scripts to render modal dialogs and supply
            // code suggestions.
            bundles.Add(new ScriptBundle("~/bundles/js/suggestions").Include(
                "~/Scripts/back-office/modal-dialog.js",
                "~/Scripts/back-office/code-suggestions.js",
                "~/Scripts/back-office/aux-selections.js"
            ));

            // Read-only version of Google Maps.
            bundles.Add(new ScriptBundle("~/bundles/js/google-maps-ro").Include(
                "~/Scripts/configs/google-maps/google-maps-ro.js"
            ));

            // Version of Google Maps for use in edit views.
            bundles.Add(new ScriptBundle("~/bundles/js/google-maps-rw").Include(
                "~/Scripts/configs/google-maps/google-maps-rw.js"
            ));

            // Bundle that contains both Quill.js (the rich HTML editor)
            // and configs for it.
            bundles.Add(new ScriptBundle("~/bundles/js/quill").Include(
                "~/Scripts/vendor/quill/quill.js",
                "~/Scripts/configs/quill/quill-config.js"
            ));

            // Bundle that contains the needed files for the date-pickers
            // to function. This Bootstrap-themed date picker uses moment.js,
            // a popular JS library to work with date-time values.
            // The necessary JS code to initialize the datepicker
            // is also included.
            bundles.Add(new ScriptBundle("~/bundles/js/date-picker").Include(
                "~/Scripts/vendor/bs-datepicker/moment.js",
                "~/Scripts/vendor/bs-datepicker/bootstrap-datetimepicker.js",
                "~/Scripts/configs/bs-datepicker/bs-datepicker-config.js"
            ));

            // Bundle that contains the necessary code to show the "Do you want to delete this?"
            // modal dialogs in list views.
            bundles.Add(new ScriptBundle("~/bundles/js/delete-modal").Include(
                "~/Scripts/back-office/delete-modal.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js/video-overlay").Include(
                "~/Scripts/frontoffice/showcase-video.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js/ajax-pagination").Include(
                "~/Scripts/common/ajax-pagination.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js/overlay-image").Include(
                "~/Scripts/frontoffice/overlay-image-carousel.js"
            ));
        }
    }
}
