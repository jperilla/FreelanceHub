using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using Web.Model;
using Web.Utilities;

namespace Web.Controllers
{
    public class AccountController : BaseController
    {

        public ActionResult Index(Account account)
        {
            return View();
        }

        public ActionResult AccountSuspended(Account account)
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
                    Account user = RavenSession.Load<Account>(login.EmailAddress.Trim());
                    if (null != user)
                    {
                        //if (user.User.ValidatePassword(login.PasswordText))
                        {
                            SetAuthenticationCookie(login.EmailAddress, login.RememberMe);
                            return Json("success");
                        }
                        //else
                            //return Json("Invalid Password");
                    }
                    else
                        return Json("User cannot be found");
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


        // GET: /SignUp/
        public ActionResult SignUp()
        {
            return View("../LandingPage/Index");
        }

        // POST: /SignUp/
        [HttpPost]
        public ActionResult SignUp(Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Since we are using a natural key for users (email address)
                    // we need this to avoid overwriting a user
                    if (null != Account.GetById(RavenSession, account.Id))
                    {
                        return PartialView("_AlreadySignedUp");
                    }
                    else
                    {
                       /* var newUser = new AuthenticationUser
                        {
                            Name = account.Name,
                            Id = String.Format("Raven/Users/{0}", account.Id),
                            AllowedDatabases = new[] { "*" }
                        }.SetPassword(account.Password);*/
                        //account.User = newUser;
                        RavenSession.Store(account);
                        
                        if (account.Plan == "Monthly")
                        {
                            return Redirect(String.Format("{0}?first_name={1}&last_name={2}&email={3}&reference={4}", Account.BASIC_MONTHLY_PLAN_URL, 
                                                    CustomerUtilities.GetFirstName(account.Name),
                                                    CustomerUtilities.GetLastName(account.Name),
                                                    account.Id,
                                                    account.Id));
                        }
                        else
                        {
                            return Redirect(String.Format("{0}?first_name={1}&last_name={2}&email={3}&reference={4}", Account.BASIC_YEARLY_PLAN_URL,
                                                    CustomerUtilities.GetFirstName(account.Name),
                                                    CustomerUtilities.GetLastName(account.Name),
                                                    account.Id,
                                                    account.Id));
                        }
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

        public virtual void SetAuthenticationCookie(string email, bool remember)
        {
            FormsAuthentication.SetAuthCookie(email, remember);
        }
    }
}
