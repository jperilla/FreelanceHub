using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using Web.Model;
using Web.Utilities;
using Griffin.MvcContrib.RavenDb.Providers;
using Griffin.MvcContrib.Providers.Membership;
using System.Collections.Generic;
using System.Linq;
using SimpleSocialAuth.MVC3;
using System.Web;
using Griffin.MvcContrib.Providers.Membership.SqlRepository;

namespace Web.Controllers
{
    public class AccountController : BaseController
    {
        private static IAccountRepository _accountRepository;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);                
            _accountRepository = new RavenDbAccountRepository(RavenSession);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AccountSuspended()
        {
            return View();
        }

        // POST: /Login/
        [HttpPost]
        public ActionResult Login(Login login)
        {
            try
            {
                if (ModelState.IsValid)                    
                {
                    // TODO: Authenticate
                    
                    LogUserIn(login.Email);
                    return View("Index", "Home");
                }
                else
                {
                    return Json("Please correct input errors.");
                }
            }
            catch (Exception ex)
            {
                return Json("Oops! Something went wrong, try again.");
            }
        }

        // Coming Back from Chargify
        public ActionResult Login(string email)
        {
            // Is there a valid id?
            if (!string.IsNullOrWhiteSpace(email))
            {
                return LogUserIn(email);              
            }

            return View("Index", "LandingPage");
        }

        private ActionResult LogUserIn(string email)
        {
            // Load the account from Raven
            int totalRecords;
            IEnumerable<IMembershipAccount> member = _accountRepository.FindByEmail(email, 1, 1, out totalRecords);
            Account account = null;
            if (member.Count() > 0)
            {
                // Load Account
                account = new Account(email);

                // Check that the account is current in Chargify
                if (account.IsAccountCurrent())
                {
                    // Set user authentication cookie
                    FormsAuthentication.SetAuthCookie(email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Go to user account page - will show suspended
                    return View("Index", "Account");
                }
            }
            else
            {
                return View("Index", "LandingPage");
            }
        }


        // GET: /SignUp/
        public ActionResult SignUp()
        {
            return View("../LandingPage/Index");
        }

        // POST: /SignUp/
        [HttpPost]
        public ActionResult SignUp(Signup signup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IMembershipAccount newMember = new MembershipAccount()
                        {
                            Email = signup.Email,
                            Password = signup.Password,
                            UserName = signup.Email
                        };
                    _accountRepository.Register(newMember);
                    //RavenSession.Store(newMember);

                    switch (signup.Plan)
                    {
                        case "full-time":
                            {
                                return Redirect(String.Format("{0}?first_name={1}&last_name={2}&email={3}&reference={4}", Account.FREELANCER_MONTHLY_PLAN_URL,
                                                        CustomerUtilities.GetFirstName(signup.Name),
                                                        CustomerUtilities.GetLastName(signup.Name),
                                                        signup.Email,
                                                        signup.Email));
                            }
                            break;
                        case "part-time":
                            {
                                return Redirect(String.Format("{0}?first_name={1}&last_name={2}&email={3}&reference={4}", Account.BUDGET_MONTHLY_PLAN_URL,
                                                        CustomerUtilities.GetFirstName(signup.Name),
                                                        CustomerUtilities.GetLastName(signup.Name),
                                                        signup.Email,
                                                        signup.Email));
                            }
                            break;
                        case "agency":
                            {
                                return Redirect(String.Format("{0}?first_name={1}&last_name={2}&email={3}&reference={4}", Account.AGENCY_MONTHLY_PLAN_URL,
                                                        CustomerUtilities.GetFirstName(signup.Name),
                                                        CustomerUtilities.GetLastName(signup.Name),
                                                        signup.Email,
                                                        signup.Email));
                            }
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                return Json("Oops! Something went wrong, try again.");
            }

            return View();
        }

        // GET: /Logout/
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("../LandingPage/Index");
        }


        #region SimpleSocialAuth

        [HttpPost]
        public ActionResult SimpleAuthenticate(AuthType authType)
        {
            var authHandler =
              AuthHandlerFactory.CreateAuthHandler(authType);

            string redirectUrl =
              authHandler
                .PrepareAuthRequest(
                  Request,
                  Url.Action("DoAuth", new { authType = (int)authType }));

            return
              Redirect(redirectUrl);
        }

        // Returning from Facebook or Google
        public ActionResult DoAuth(AuthType authType)
        {
            var authHandler =
              AuthHandlerFactory.CreateAuthHandler(authType);

            var userData =
              authHandler
                .ProcessAuthRequest(Request as HttpRequestWrapper);

            if (userData == null)
            {
                TempData["authError"] =
                  "Authentication has failed.";

                return
                  RedirectToAction("LogIn");
            }

            FormsAuthentication.SetAuthCookie(userData.Email, true);

            Account account = RavenSession.Load<Account>(userData.Email);
            
            // Check if subscription is current, if not redirect to account page
            // for user to update their subscription
            if (!account.IsAccountCurrent())
            {
                return RedirectToAction("AccountSuspended", "Account");
            }


            return
              Session["ReturnUrl"] != null
              ? (ActionResult)Redirect((string)Session["ReturnUrl"])
              : RedirectToAction("Index", "Home");
        }

        #endregion

        public virtual void SetAuthenticationCookie(string email, bool remember)
        {
            FormsAuthentication.SetAuthCookie(email, remember);
        }
    }
}
