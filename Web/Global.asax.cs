using System.Web.Mvc;
using System.Web.Routing;
using Raven.Client.Document;
using Raven.Client.Indexes;
using System.Reflection;
using Raven.Database.Server;
using Raven.Client;
using Raven.Client.Embedded;


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

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "LandingPage", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            // Initialize container for ravendb membership provider
            /*var builder = new ContainerBuilder();
            builder.RegisterType<HashPasswordStrategy>().AsImplementedInterfaces();
            builder.RegisterType<RavenDbAccountRepository>().AsImplementedInterfaces()
            builder.RegisterInstance(new PasswordPolicy
            {
                IsPasswordQuestionRequired = false,
                IsPasswordResetEnabled = true,
                IsPasswordRetrievalEnabled = false,
                MaxInvalidPasswordAttempts = 5,
                MinRequiredNonAlphanumericCharacters = 0,
                PasswordAttemptWindow = 10,
                PasswordMinimumLength = 6,
                PasswordStrengthRegularExpression = null
            }).AsImplementedInterfaces();
            //Register all controllers of the current Assembly
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.Register(c => c.For<IDocumentStore>().Singleton().Use(Store));
            builder.Register(c => c.For<IDocumentSession>().Use(ctx => ctx.GetInstance<IDocumentStore>().OpenSession());

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));*/

            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);


            
            if (Store == null)
            {
                /* Initialize the Document Store - for local db only 
                Store = new EmbeddableDocumentStore
                {
                    RunInMemory = true,
                    ConnectionStringName = "RavenDB",
                    UseEmbeddedHttpServer = true,
                    Configuration = { Port = 8080 }

                };
                NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
                Store.Initialize(); */

                /* for the server*/
                Store = new DocumentStore { ConnectionStringName = "RavenDB" };
                Store.Initialize();

                IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), Store);
            }            
        }
    }
}