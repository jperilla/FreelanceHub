
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
                // Load Account
                var account = new Account(User.Identity.Name);
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
