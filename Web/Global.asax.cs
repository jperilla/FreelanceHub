using System.Web.Mvc;
using System.Web.Routing;
using Raven.Client.Document;
using Raven.Client.Indexes;
using System.Reflection;
using Raven.Database.Server;
using Raven.Client;
using Raven.Client.Embedded;
using Web.Factories;
using System.Web.Configuration;


namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        // Create a singleton dependency injection container
        public static IDocumentStore Store { get; set; }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Content/xml/*");
            routes.IgnoreRoute("Content/html/*");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "LandingPage", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            Bootstrapper.Initialise();
        }
    }
}