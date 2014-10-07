using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Models;
using Raven.Client;
using System.Text;
using System.Web.Security;
using Web.Attribute;

namespace Web.Controllers
{
    [CustomAuthorize(Roles = "Basic, Agency, FullTime, PartTime, Administrator")]
    public class BookmarkletController : BaseController
    {
        public BookmarkletController(IDocumentSession documentSession)
            : base(documentSession)
        {
        }

        //
        // GET: /Login/
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        //
        // Post: /Login/
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
                        redirect = null;
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(login.Email, false); // TODO: Role will be suspended, so that they can't get anywhere else
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

            return Json(new { Success = 0, Message = totalError.ToString() });
        }

        //
        // GET: /SaveExternalJob/

        [AllowAnonymous]
        public ActionResult SaveExternalJob(string url, string title, string description)
        {
            if (!User.Identity.IsAuthenticated)
            {
                var login = new BookmarkletLogin();
                login.Description = description;
                login.Title = title;
                login.URL = url;
                return View("Login", login);
            }

            if (url == null || title == null)
            {
                return View("Error", null, "We were not able to locate a job on this page.");
            }

            // Check if job was already saved
            // Load the current account
            Account account = Account.GetAccount(User.Identity.Name, RavenSession);
            Job job = null;
            if (account != null)
            {
                var jobs = from j in account.Jobs
                           where j.URL == url
                           select j;

                if (jobs != null && jobs.Count() > 0)
                {
                    return View("Error", null, "You have already saved this job.");
                }

                // Check if the user has met their saved job limit
                if (account.HasMetSavedJobLimit())
                {
                    return View("Upgrade");
                }

                // Create the job
                job = new Job
                {
                    JobStatus = new JobStatus(),
                    ShortDescription = description,
                    Title = title,
                    URL = url
                };
                job.JobStatus.Status = "Lead";

                if (account != null)
                {
                    if (account.Jobs == null)
                        account.Jobs = new List<Job>();

                    account.Jobs.Add(job);
                    RavenSession.Store(account);
                }
            }

            return View("Details", job);
        }

        //
        // GET /Details/url
        public ViewResult Details(string url)
        {
            return View(RavenSession.Load<Job>(url));
        }

    }


}
