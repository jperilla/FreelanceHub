using System.Web.Mvc;
using Web.Models;
using System.Web.Security;
using Raven.Client;

namespace Web.Controllers
{
    public class LandingPageController : BaseController
    {
        public LandingPageController(IDocumentSession documentSession)
            : base(documentSession)
        {
        }

        //
        // GET: /Index/
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Load Account
                var account = Account.GetAccount(User.Identity.Name, RavenSession);
                return View("_Home", account);
            }

            return View();
        }

        //
        // GET: /Index/
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Load Account
                var account = Account.GetAccount(User.Identity.Name, RavenSession);
                return View("_Home", account);
            }

            return View();
        }
    }
}
