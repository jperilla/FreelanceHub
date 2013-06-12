
using System.Web.Mvc;
using Web.Models;
using Raven.Client;
using Web.Attribute;

namespace Web.Controllers
{
    [CustomAuthorize(Roles = "Basic, Agency, FullTime, PartTime, Administrator")]
    public class HomeController : BaseController
    {
        public HomeController(IDocumentSession documentSession)
            : base(documentSession)
        {
        }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Load Account
                var account = Account.GetAccount(User.Identity.Name, RavenSession);
                return View("_Home", account);
            }

            return View("Index", "Landing Page");
        }

        //
        // GET: /Error/
        public ActionResult Error()
        {
            return View();
        }

    }
}
