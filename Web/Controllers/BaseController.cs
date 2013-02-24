using System.Web.Mvc;
using Raven.Client;

namespace Web.Controllers
{   
    [HandleError]
    public abstract class BaseController : Controller
    {
        public IDocumentSession RavenSession { get; private set; }

        protected BaseController(IDocumentSession documentSession)
        {
            RavenSession = documentSession;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            using (RavenSession)
            {
                if (filterContext.Exception != null)
                    return;

                if (RavenSession != null)
                    RavenSession.SaveChanges();
            }
        }
    }
}
