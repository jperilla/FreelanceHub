using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using Raven.Bundles.Authentication;
using Web.Model;

namespace Web.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /SignIn/
        public ActionResult Login(Account account)
        {
            MvcApplication.Store.Credentials = new NetworkCredential(account.Id, account.Password);
            RavenSession.Store(new AuthenticationUser
            {
                Name = account.Name,
                Id = String.Format("Raven/Users/{0}", account.Id),
                AllowedDatabases = new[] { "*" }
            }.SetPassword(account.Password));

            return View("_Home", account);
        }

        public ActionResult FacebookLogin(string name, string email)
        {
            Account newAccount = new Account();
            var newUser = new AuthenticationUser
            {
                Name = name,
                Id = String.Format("Raven/Users/{0}", email),
                AllowedDatabases = new[] { "*" }
            };
            newAccount.User = newUser;
            newAccount.FacebookLogin = true;
            RavenSession.Store(newAccount);
            SetAuthenticationCookie(email, true);
            return View("../LandingPage/Index");
        }

        // GET: /SignUp/
        public ActionResult SignUp()
        {
            return View("../LandingPage/Index");
        }

        // POST: /SignUp/
        [HttpPost]
        public ActionResult SignUp(Account account)
        {
            if(ModelState.IsValid)
            {
                // Since we are using a natural key for users (email address)
                // we need this to avoid overwriting a user
                if (null != Account.GetById(RavenSession, account.Id))
                {
                    return Json("This email address is already in use.");
                }
                else
                {
                    var newUser = new AuthenticationUser
                    {
                        Name = account.Name,
                        Id = String.Format("Raven/Users/{0}", account.Id),
                        AllowedDatabases = new[] { "*" }
                    }.SetPassword(account.Password);
                    account.User = newUser;
                    RavenSession.Store(account);
                    SetAuthenticationCookie(account.Id, account.RemeberMe);
                    return Json("success");
                }
                
            }
               
            return Json("Oops! Something went wrong, try again.");
        }

        // GET: /Logout/
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("../LandingPage/Index");
        }

        public virtual void SetAuthenticationCookie(string email, bool remember)
        {
            FormsAuthentication.SetAuthCookie(email, remember);
        }
    }
}
