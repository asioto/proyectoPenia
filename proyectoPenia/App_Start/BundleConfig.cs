using System.Web.Optimization;

namespace IdentitySample
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Vendor/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~/Vendor/tether.min.js",
                        "~/Vendor/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/mdk").Include(
                        "~/Vendor/dom-factory.js",
                        "~/Vendor/material-design-kit.js"));

            bundles.Add(new ScriptBundle("~/bundles/sidebarcollapse").Include(
                       "~/Vendor/sidebar-collapse.js"));

            bundles.Add(new ScriptBundle("~/bundles/appjs").Include(
                        "~/Scripts/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css",
                      "~/Vendor/material-design-kit.css",
                      "~/Vendor/sidebar-collapse.min.css"
                      ).Include("~/Content/material-icons.css", new CssRewriteUrlTransform()).Include(
                      "~/Content/site.css"));
        }
    }
}
