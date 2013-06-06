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
using ChargifyNET;
using System.Text;
using Web.Attribute;

namespace Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {

        public AccountController(IDocumentSession documentSession)
            : base(documentSession)
        {
        }

        public ActionResult Index(Account account)
        {
            if(account == null)
                account = Account.GetAccount(User.Identity.Name, RavenSession);
            else if (account.Email == null && User.Identity.IsAuthenticated)
            {
                account.Email = User.Identity.Name;
            }

            if (account != null)
            {
                account.LoadChargifyInfo();
                return View(account);
            }

            return View("../LandingPage/Index");
        }

        // POST: /Login/
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Login login)
        {
            StringBuilder totalError = new StringBuilder();
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(login.Email, login.Password))
                {
                    Account account = Account.GetAccount(login.Email, RavenSession);
                    string redirect = null;
                    if (account != null && account.IsAccountCurrent())
                    {
                        // Set user authentication cookie
                        FormsAuthentication.SetAuthCookie(login.Email, false);
                        redirect = Url.Action("Index", "Home");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(login.Email, false);
                        // Set role to inactive, if not already
                        string[] roles = Roles.GetRolesForUser(login.Email);
                        if(null != roles && roles.Count() > 0)
                            Roles.RemoveUserFromRoles(login.Email, roles);
                        Roles.AddUserToRole(login.Email, Account.INACTIVE_ROLE);
                        redirect = Url.Action("Index", "Account");
                    }

                    return Json(new { Success = 1, Redirect = redirect });
                }
                else
                {
                    totalError.Append("Invalid Email or Password.");
                }
            }
            else
            {
                foreach (var obj in ModelState.Values)
                {
                    foreach (var error in obj.Errors)
                    {
                        if (!string.IsNullOrEmpty(error.ErrorMessage))
                        {
                            totalError.Append(error.ErrorMessage + "<br />");
                        }
                    }
                }
            }

            return Json(new { Success = 0, Message = totalError.ToString()});
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

            return RedirectToAction("Login", "LandingPage");
        }

        private ActionResult LogUserIn(string email)
        {
            Account account = Account.GetAccount(email, RavenSession);
            if(account != null && account.IsAccountCurrent())
            {
                // Set user authentication cookie
                FormsAuthentication.SetAuthCookie(email, false);

                // Set the user's role
                ISubscription customerSubscription = account.CustomerSubscriptions.Values.FirstOrDefault();                
                if(customerSubscription.Product.Handle == Account.FREE_PLAN_HANDLE)
                {
                   Roles.AddUserToRole(email, Account.BASIC_ROLE);
                }
                else if (customerSubscription.Product.Handle == Account.BUDGET_MONTHLY_PLAN_HANDLE)
                {
                    Roles.AddUserToRole(email, Account.PARTTIME_ROLE);
                }
                else if (customerSubscription.Product.Handle == Account.FREELANCER_MONTHLY_PLAN_HANDLE)
                {
                    Roles.AddUserToRole(email, Account.FULLTIME_ROLE);
                }
                else if (customerSubscription.Product.Handle == Account.AGENCY_MONTHLY_PLAN_HANDLE)
                {
                    Roles.AddUserToRole(email, Account.AGENCY_ROLE);
                }

                return RedirectToAction("Index", "Home", account);
            }
            
            // Go to user account page - will show suspended, user must take action
            return RedirectToAction("Index", account);
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
            StringBuilder totalError = new StringBuilder();
            if (ModelState.IsValid)
            {
                // Check for existing account
                var existingAccount = Account.GetAccount(signup.Email, RavenSession);
                if (existingAccount != null)
                {
                    return Json(new { Success = 0, Message = "User with email " + signup.Email + " already exists."});
                }

                // Redirect to Chargify Payment Page
                MembershipUser newMember = Membership.CreateUser(signup.Email, signup.Password);
                var newAccount = new Account(signup.Email);
                RavenSession.Store(newAccount);

                string redirect = null;
                switch (signup.Plan)
                {
                    case "free":
                        {
                            redirect = String.Format("{0}?first_name={1}&last_name={2}&email={3}&reference={4}", Account.FREE_PLAN_URL,
                                                    CustomerUtilities.GetFirstName(signup.Name),
                                                    CustomerUtilities.GetLastName(signup.Name),
                                                    signup.Email,
                                                    signup.Email);
                        }
                        break;
                    case "full-time":
                        {
                            redirect = String.Format("{0}?first_name={1}&last_name={2}&email={3}&reference={4}", Account.FREELANCER_MONTHLY_PLAN_URL,
                                                    CustomerUtilities.GetFirstName(signup.Name),
                                                    CustomerUtilities.GetLastName(signup.Name),
                                                    signup.Email,
                                                    signup.Email);
                        }
                        break;
                    case "part-time":
                        {
                            redirect = String.Format("{0}?first_name={1}&last_name={2}&email={3}&reference={4}", Account.BUDGET_MONTHLY_PLAN_URL,
                                                    CustomerUtilities.GetFirstName(signup.Name),
                                                    CustomerUtilities.GetLastName(signup.Name),
                                                    signup.Email,
                                                    signup.Email);
                        }
                        break;
                    case "agency":
                        {
                            redirect = String.Format("{0}?first_name={1}&last_name={2}&email={3}&reference={4}", Account.AGENCY_MONTHLY_PLAN_URL,
                                                    CustomerUtilities.GetFirstName(signup.Name),
                                                    CustomerUtilities.GetLastName(signup.Name),
                                                    signup.Email,
                                                    signup.Email);
                        }
                        break;
                }

                return Json(new { Success = 1, Redirect = redirect });

            }
            else
            {
                foreach (var obj in ModelState.Values)
                {
                    foreach (var error in obj.Errors)
                    {
                        if (!string.IsNullOrEmpty(error.ErrorMessage))
                        {
                            totalError.Append(error.ErrorMessage + "<br />");
                        }
                    }
                }
            }

            return Json(new { Success = 0, Message = totalError.ToString() });
        }

        // GET: /Logout/
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("../LandingPage/Index");
        }

        public ActionResult Upgrade(Account account)
        {
            return View(account);
        }

        public ActionResult ChangePlanToFree(string email)
        {
            Account account = new Account(email);

            // If plan changed, update customer subscription
            if (account != null && account.CustomerSubscriptions != null && account.CustomerSubscriptions.Count > 0)
            {
                ISubscription customerSubscription = account.CustomerSubscriptions.Values.FirstOrDefault();
                if (customerSubscription.Product.Handle != Account.FREE_PLAN_HANDLE)
                {
                    Account.Chargify.EditSubscriptionProduct(customerSubscription.SubscriptionID, Account.FREE_PLAN_HANDLE);
                    string[] roles = Roles.GetRolesForUser(email);
                    if (null != roles && roles.Count() > 0)
                        Roles.RemoveUserFromRoles(email, roles);
                    Roles.AddUserToRole(email, Account.BASIC_ROLE);
                }
            }

            // Go back to account view
            return RedirectToAction("Index", account);
        }

        public ActionResult ChangePlanToBudget(string email)
        {
            Account account = new Account(email);

            // If plan changed, update customer subscription
            if (account != null && account.CustomerSubscriptions != null && account.CustomerSubscriptions.Count > 0)
            {
                ISubscription customerSubscription = account.CustomerSubscriptions.Values.FirstOrDefault();
                if (customerSubscription.Product.Handle != Account.BUDGET_MONTHLY_PLAN_HANDLE)
                {
                    Account.Chargify.EditSubscriptionProduct(customerSubscription.SubscriptionID, Account.BUDGET_MONTHLY_PLAN_HANDLE);
                    Roles.RemoveUserFromRoles(email, Roles.GetRolesForUser());
                    Roles.AddUserToRole(email, Account.PARTTIME_ROLE);
                }

                // If they are currently on the free plan, force them to enter a credit card
                if (customerSubscription.Product.Handle == Account.FREE_PLAN_HANDLE)
                {
                    return new RedirectResult (account.UpdatePaymentLink);
                }                
            }

            // Go back to account view
            return RedirectToAction("Index", account);
        }

        public ActionResult ChangePlanToFreelancer(string email)
        {
            Account account = new Account(email);

            // If plan changed, update customer subscription
            if (account != null && account.CustomerSubscriptions != null && account.CustomerSubscriptions.Count > 0)
            {
                ChargifyNET.ISubscription customerSubscription = account.CustomerSubscriptions.Values.FirstOrDefault();
                if (customerSubscription.Product.Handle != Account.FREELANCER_MONTHLY_PLAN_HANDLE)
                {
                    Account.Chargify.EditSubscriptionProduct(customerSubscription.SubscriptionID, Account.FREELANCER_MONTHLY_PLAN_HANDLE);
                    string[] roles = Roles.GetRolesForUser(email);
                    if (null != roles && roles.Count() > 0)
                        Roles.RemoveUserFromRoles(email, roles);
                    Roles.AddUserToRole(email, Account.FULLTIME_ROLE);
                }

                // If they are currently on the free plan, force them to enter a credit card
                if (customerSubscription.Product.Handle == Account.FREE_PLAN_HANDLE)
                {
                    return new RedirectResult(account.UpdatePaymentLink);
                }
            }

            // Go back to account view
            return RedirectToAction("Index", account);
        }

        public ActionResult ChangePlanToAgency(string email)
        {
            Account account = new Account(email);

            // If plan changed, update customer subscription
            if (account != null && account.CustomerSubscriptions != null && account.CustomerSubscriptions.Count > 0)
            {
                ChargifyNET.ISubscription customerSubscription = account.CustomerSubscriptions.Values.FirstOrDefault();
                if (customerSubscription.Product.Handle != Account.AGENCY_MONTHLY_PLAN_HANDLE)
                {
                    Account.Chargify.EditSubscriptionProduct(customerSubscription.SubscriptionID, Account.AGENCY_MONTHLY_PLAN_HANDLE);
                    string[] roles = Roles.GetRolesForUser(email);
                    if (null != roles && roles.Count() > 0)
                        Roles.RemoveUserFromRoles(email, roles);
                    Roles.AddUserToRole(email, Account.AGENCY_ROLE);
                }

                // If they are currently on the free plan, force them to enter a credit card
                if (customerSubscription.Product.Handle == Account.FREE_PLAN_HANDLE)
                {
                    return new RedirectResult(account.UpdatePaymentLink);
                }
            }

            // Go back to account view
            return RedirectToAction("Index", account);
        }

        public ActionResult Cancel(Account account)
        {
            return View(account);
        }

        public ActionResult ReallyCancel(String email)
        {
            Account account = new Account(email);

            // If customer has a current subscription, cancel it
            if (account != null && account.CustomerSubscriptions != null && account.CustomerSubscriptions.Count > 0)
            {
                ChargifyNET.ISubscription customerSubscription = account.CustomerSubscriptions.Values.FirstOrDefault();
                if (customerSubscription.State != SubscriptionState.Canceled)
                {
                    Account.Chargify.UpdateDelayedCancelForSubscription(customerSubscription.SubscriptionID, true, "");
                }
            }

            // Go back to account view
            return RedirectToAction("Index", account);
        }

        public ActionResult Reactivate(String email)
        {
            Account account = new Account(email);

            // If customer has a canceled subscription, reactivate it
            if (account != null && account.CustomerSubscriptions != null && account.CustomerSubscriptions.Count > 0)
            {
                ChargifyNET.ISubscription customerSubscription = account.CustomerSubscriptions.Values.FirstOrDefault();
                if (customerSubscription.State == SubscriptionState.Canceled || 
                    customerSubscription.State == SubscriptionState.Expired ||
                    customerSubscription.State == SubscriptionState.Trial_Ended ||
                    customerSubscription.State == SubscriptionState.Unknown ||
                    customerSubscription.DelayedCancelAt.Year != 0001)
                {
                    Account.Chargify.ReactivateSubscription(customerSubscription.SubscriptionID, false);
                    string[] roles = Roles.GetRolesForUser(email);
                    if (null != roles && roles.Count() > 0)
                        Roles.RemoveUserFromRoles(email, roles);
                    if (customerSubscription.Product.Handle == Account.FREE_PLAN_HANDLE)
                    {
                        Roles.AddUserToRole(email, Account.BASIC_ROLE);
                    }
                    else if (customerSubscription.Product.Handle == Account.BUDGET_MONTHLY_PLAN_HANDLE)
                    {
                        Roles.AddUserToRole(email, Account.PARTTIME_ROLE);
                    }
                    else if (customerSubscription.Product.Handle == Account.FREELANCER_MONTHLY_PLAN_HANDLE)
                    {
                        Roles.AddUserToRole(email, Account.FULLTIME_ROLE);
                    }
                    else if (customerSubscription.Product.Handle == Account.AGENCY_MONTHLY_PLAN_HANDLE)
                    {
                        Roles.AddUserToRole(email, Account.AGENCY_ROLE);
                    }
                }
            }

            // Go back to account view
            return RedirectToAction("Index", account);
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
                return RedirectToAction("Index");
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
