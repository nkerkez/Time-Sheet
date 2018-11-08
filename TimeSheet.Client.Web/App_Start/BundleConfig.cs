using System.Web;
using System.Web.Optimization;

namespace TimeSheet.Client.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    //    "~/Scripts/jquery-{version}.js",
                   //     "~/Scripts/libs/jquery-ui-1.9.2.custom.min.js",
                        "~/Scripts/libs/jquery.fancybox.js",
                       "~/Scripts/main/jquery-1.8.3.min.js",
                       "~/Scripts/jquery.validate.min.js",
                       "~/Scripts/jquery.validate.unobtrusive.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
                        "~/Scripts/main/accordion.js",
                        "~/Scripts/main/default.js",
                        "~/Scripts/main/modernizr-2.6.2.min.js"));
            /*
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
*/
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            /*
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
*/
            bundles.Add(new StyleBundle("~/Content/css").Include(
                 //     "~/Content/bootstrap.css",
                 //     "~/Content/site.css",
                      "~/Content/style.css"));
            //   "~/Content/lib/fancybox.css"
        }
    }
}
