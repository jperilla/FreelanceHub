using System.Web.Mvc;
using Raven.Client;
using Web.Logging;

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

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
                base.OnException(filterContext);

            using (RavenSession)
            {
                Logging.Logger.LogException(filterContext.Exception, RavenSession);
            }

            if (filterContext.HttpContext.IsCustomErrorEnabled)
            {
                // If the global handle error filter is enabled then this is not needed.
                filterContext.ExceptionHandled = true;
                this.View("Error").ExecuteResult(this.ControllerContext);
            }
        }
    }
}
