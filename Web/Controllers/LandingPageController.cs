using System.Web.Mvc;
using Web.Model;
using System.Web.Security;

namespace Web.Controllers
{
    public class LandingPageController : BaseController
    {
        //
        // GET: /Index/
        public ActionResult Index(string id)
        {
            string userEmail = string.IsNullOrWhiteSpace(id) ? User.Identity.Name : id;
            return SignIn(userEmail);
        }

        private ActionResult SignIn(string id)
        {
            // Is there a valid id?
            if(!string.IsNullOrWhiteSpace(id))
            {
                // Set user authentication cookie
                FormsAuthentication.SetAuthCookie(id, false);
            
                Account authenticatedAccount = RavenSession.Load<Account>(id);
                if (null != authenticatedAccount)
                {
                    // Check if subscription is current
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

            return View("Index");
        }
    }
}
