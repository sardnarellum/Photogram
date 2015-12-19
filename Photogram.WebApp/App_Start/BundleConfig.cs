using System.Web;
using System.Web.Optimization;

namespace Photogram.WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/lazyload").Include(
                        "~/Scripts/blazy.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/waitforimages").Include(
                        "~/Scripts/jquery.waitforimages.js",
                        "~/Scripts/jquery.waitforimages.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusive-ajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new StyleBundle("~/Content/cover").Include(
                      "~/Content/cover.css"));

            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                      "~/Content/font-awesome.css",
                      "~/Content/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/grayscale").Include(
                      "~/Content/grayscale.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/misc.css"));

            bundles.Add(new ScriptBundle("~/bundles/grayscale").Include(
                      "~/Scripts/grayscale.js"));

            bundles.Add(new ScriptBundle("~/bundles/filestyle").Include(
                    "~/Scripts/bootstrap-filestyle.js"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                    "~/Scripts/Chart.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/packery").Include(
                    "~/Scripts/packery.pkgd.js"));
        }
    }
}
