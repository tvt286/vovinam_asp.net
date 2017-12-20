using System.Web;
using System.Web.Optimization;

namespace Vovinam
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Vendor scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery.min.js"));
         
            // jQuery Validation
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
           "~/Scripts/jquery.validate.min.js", "~/Scripts/jquery.unobtrusive-ajax.js", "~/Scripts/jquery.validate.unobtrusive.js", "~/Scripts/common.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/removeads.js", "~/Scripts/jquery.core.js", "~/Scripts/jquery.app.js",
                      "~/Scripts/bootstrap.min.js", "~/Scripts/moment-with-locales.js", "~/Scripts/bootstrap-datetimepicker.js", "~/Scripts/autoNumeric-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/login").Include("~/Scripts/modernizr.js", 
                     "~/Scripts/detect.js", "~/Scripts/fastclick.js", "~/Scripts/jquery.blockUI.js", "~/Scripts/waves.js", "~/Scripts/jquery.slimscroll.js", "~/Scripts/jquery.scrollTo.min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                      "~/Scripts/app/inspinia.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Scripts/plugins/slimScroll/jquery.slimscroll.min.js"));
            // toast
            bundles.Add(new ScriptBundle("~/plugins/toastr").Include(
                      "~/Scripts/plugins/toastr/toastr.min.js", "~/Scripts/jquery.toastr.js"));


            // jQuery plugins
            bundles.Add(new ScriptBundle("~/plugins/metsiMenu").Include(
                      "~/Scripts/plugins/metisMenu/metisMenu.min.js"));

            bundles.Add(new ScriptBundle("~/plugins/pace").Include(
                      "~/Scripts/plugins/pace/pace.min.js"));

            bundles.Add(new ScriptBundle("~/plugins/sweetalert").Include(
                      "~/Scripts/plugins/sweetalert/sweetalert.min.js"));

            bundles.Add(new ScriptBundle("~/plugins/select2").Include(
                      "~/Scripts/plugins/select2/select2.full.min.js"));

            bundles.Add(new ScriptBundle("~/plugins/chosen").Include(
                      "~/Scripts/plugins/chosen/chosen.jquery.js"));

            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/animate.css",
                /*"~/Content/style.css",*/ "~/Content/bootstrap-datetimepicker.css", "~/Content/fix.css",
                      "~/Content/plugin/sweetalert/sweetalert.css", "~/Content/plugin/toastr/toastr.min.css", "~/Content/plugin/select2/select2.min.css",
                      "~/Content/plugin/chosen/chosen.css", "~/Content/Plugin/boostrap-tagsinput/bootstrap-tagsinput.css", "~/Content/nv.d3.css"));


            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

        }
    }
}
