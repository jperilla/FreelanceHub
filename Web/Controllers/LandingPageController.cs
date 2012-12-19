using System.Web.Mvc;
using Web.Model;

namespace Web.Controllers
{
    public class LandingPageController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                Account authenticatedAccount = RavenSession.Load<Account>(User.Identity.Name);
                if (null != authenticatedAccount)
                {
                    return RedirectToAction("Index", "Home", authenticatedAccount);
                }
            }

            return View();
        }

    }
}
