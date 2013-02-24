using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;
using Web.Utilities;
using System.Collections.Generic;
using System.Linq;
using SimpleSocialAuth.MVC3;
using System.Web;
using Raven.Client;

namespace Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {

        public AccountController(IDocumentSession documentSession)
            : base(documentSession)
        {
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
        [AllowAnonymous]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                if(Membership.ValidateUser(login.Email, login.Password))
                {
                    return LogUserIn(login.Email);
                }
            }

            return Json("Invalid email or password");
        }

        // Coming Back from Chargify
        [AllowAnonymous]
        public ActionResult Login(string email)
        {
            // Is there a valid id?
            if (!string.IsNullOrWhiteSpace(email))
            {
                return LogUserIn(email);              
            }

            return RedirectToAction("Index", "LandingPage");
        }

        private ActionResult LogUserIn(string email)
        {
            Account account = Account.GetAccount(email, RavenSession);
            if(account.IsAccountCurrent())
            {
                // Set user authentication cookie
                FormsAuthentication.SetAuthCookie(email, false);
                return RedirectToAction("Index", "Home");
            }
            
            // Go to user account page - will show suspended, user must take action
            return View("Index");
        }


        // GET: /SignUp/
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View("../LandingPage/Index");
        }

        // POST: /SignUp/
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignUp(Signup signup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check for existing account
                    var existingAccount = Account.GetAccount(signup.Email, RavenSession);
                    if (existingAccount != null)
                        return Json("User already exists");

                    MembershipUser newMember = Membership.CreateUser(signup.Email, signup.Password);
                    var newAccount = new Account(signup.Email);
                    RavenSession.Store(newAccount);

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
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("../LandingPage/Index");
        }


        #region SimpleSocialAuth

        [HttpPost]
        [AllowAnonymous]
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
        [AllowAnonymous]
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

        [AllowAnonymous]
        public virtual void SetAuthenticationCookie(string email, bool remember)
        {
            FormsAuthentication.SetAuthCookie(email, remember);
        }
    }
}
