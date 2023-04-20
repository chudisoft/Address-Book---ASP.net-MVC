using System.Web;
using System.Web.Optimization;

namespace Address_Book
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/modernizr-*",
                "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/themes/base/jquery-ui.css",
                "~/Content/themes/base/datepicker.css",
                "~/Content/themes/base/jquery-ui.min.css",
                "~/Content/bootstrap.css",
                "~/Content/site.css"));
        }
    }
}
