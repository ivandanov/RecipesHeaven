using System.Web;
using System.Web.Optimization;

namespace RecipesHeaven.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleConfig.RegisterScriptsBundles(bundles);
            BundleConfig.RegisterStylesBundles(bundles);
        }

        private static void RegisterScriptsBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                  "~/Scripts/jquery.dataTables.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/raty").Include(
                  "~/Scripts/jquery.raty.js"));
        }

        private static void RegisterStylesBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/page-styles").Include(
                      "~/Content/page-styles.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/dataTables").Include(
                  "~/Content/jquery.dataTables.min.css"));
            bundles.Add(new StyleBundle("~/Content/raty").Include(
                  "~/Content/jquery.raty.css"));
        }
    }
}
