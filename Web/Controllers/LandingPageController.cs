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
                    if (authenticatedAccount.IsAccountCurrent())
                    {
                        return RedirectToAction("Index", "Home", authenticatedAccount);
                    }
                    else
                    {
                        return RedirectToAction("AccountSuspended", "Account");
                    }
                }
            }

            return View();
        }

    }
}
