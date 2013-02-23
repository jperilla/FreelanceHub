using System.Web.Mvc;
using Web.Model;
using System.Web.Security;

namespace Web.Controllers
{
    public class LandingPageController : BaseController
    {
        //
        // GET: /Index/
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Load Account
                var account = new Account(User.Identity.Name);
                return View("_Home", account);
            }

            return View();
        }
    }
}
