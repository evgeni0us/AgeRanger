using System.Web;
using System.Web.Optimization;

namespace AgeRanger
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/Scripts/jquery/dist/jquery.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap/dist/js/bootstrap.min.js",
                "~/Scripts/respond/dest/respond.min.js")

                );

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angularjs/angular.min.js",
                "~/Scripts/angular-route/angular-route.js",
                "~/Scripts/angular-sanitize/angular-sanitize.min.js",
                "~/Scripts/angular-resource/angular-resource.min.js",
                "~/Scripts/angular-ui/build/angular-ui.min.js",
                "~/Scripts/angular-bootstrap/ui-bootstrap-tpls.min.js",
                "~/Scripts/bootbox/bootbox.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Scripts/bootstrap/dist/css/bootstrap.min.css",
                "~/Scripts/angular-ui/angular-ui.min.css",
                "~/Scripts/angular-bootstrap/ui-bootstrap-csp.css",
                "~/Scripts/font-awesome/css/font-awesome.min.css",
                "~/Content/site.css"

                ));

            bundles.Add(new StyleBundle("~/styles/app").Include(
                "~/app/styles/main.css"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/app/app.js",
                "~/app/app.routes.js",
                "~/app/services/agerangerservice.js",
                "~/app/views/people.js",
                "~/app/views/person.js"));
            
            
            
        }
    }
}
