using System.Web;
using System.Web.Optimization;

namespace HappyHut
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include("~/Scripts/angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular-ui")
                .IncludeDirectory("~/Scripts/angular-ui", "*.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/HappyHutStyles/Layout.css",
                      "~/Content/HappyHutStyles/HomeMain.css",
                      "~/Content/agency.css"));

            bundles.Add(new ScriptBundle("~/bundles/HappyHutJsFiles/mainCshtml.js")
                .Include("~/Scripts/HappyHutJsFiles/mainCshtml.js"));
            bundles.Add(new ScriptBundle("~/bundles/HappyHutJsFiles/enterFormData.js")
                .Include("~/Scripts/HappyHutJsFiles/enterFormData.js"));
            bundles.Add(new ScriptBundle("~/bundles/HappyHutJsFiles/layoutCshtml.js")
                .Include("~/Scripts/HappyHutJsFiles/layoutCshtml.js"));

            bundles.Add(new ScriptBundle("~/bundles/J-QueryValidations")
                .Include("~/Scripts/jquery.validate.js", "~/Scripts/jquery.validate.unobtrusive.js"));
        }
    }
}
