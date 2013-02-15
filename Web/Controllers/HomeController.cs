
using System.Web.Mvc;
using Web.Model;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("_Home", RavenSession.Load<Account>(User.Identity.Name));
            }

            return View("Index", "Landing Page");
        }

    }
}
