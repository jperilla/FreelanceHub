using System.Web.Mvc;
using System.Web.Routing;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using System.Reflection;
using Raven.Database.Server;

namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static DocumentStore Store { get; set; }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

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

            /* Initialize the Document Store - for local db only 

            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
            Store = new EmbeddableDocumentStore
            {
                ConnectionStringName = "RavenDB"//,
                //UseEmbeddedHttpServer = true

            };

            Store.Initialize();
*/
            /* for the server*/
            Store = new DocumentStore { ConnectionStringName = "RavenDB" };
            Store.Initialize();

            IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), Store);
        }
    }
}