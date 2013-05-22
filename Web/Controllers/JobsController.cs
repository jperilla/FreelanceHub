using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Raven.Client;
using Bing;

namespace Web.Controllers
{ 
    [Authorize]
    public class JobsController : BaseController
    {
        public JobsController(IDocumentSession documentSession)
            : base(documentSession)
        {
        }
        // 
        // GET: /Jobs/

        public ViewResult Index()
        {
            // Load the current account
            Account account = Account.GetAccount(User.Identity.Name, RavenSession);
            if (account != null)
            {
                return View(account.Jobs);
            }

            return View();
        }

        public JsonResult SaveFavorite(Job jobClicked)
        {
            // Check if job was already saved
            // Load the current account
            Account account = Account.GetAccount(User.Identity.Name, RavenSession);
            if (account != null)
            {
                var jobs = from j in account.Jobs
                           where j.URL == jobClicked.URL
                           select j;

                if (jobs != null && jobs.Count() > 0)
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }

                // Create the job
                var job = new Job
                {
                    JobStatus = new JobStatus(),
                    ShortDescription = jobClicked.ShortDescription,
                    Title = jobClicked.Title,
                    URL = jobClicked.URL
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

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveExternalJob(string url, string title, string description)
        {
            if (url == null || title == null)
            {
                return View("JobBookmarkletError", null, "We were not able to locate a job on this page.");
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
                    return View("JobBookmarkletError", null, "You have already saved this job.");
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

        public JsonResult UnsaveFavorite(string url)
        {
            // Load the current account
            Account account = Account.GetAccount(User.Identity.Name, RavenSession);
            if (account != null)
            {
                // Load the job
                var jobs = from j in account.Jobs
                           where j.URL == url
                           select j;

                // Delete the job
                if (jobs != null && jobs.Count() > 0)
                {
                    Job job = jobs.First();
                    account.Jobs.Remove(job);
                    RavenSession.Store(account);
                }
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult UnsaveBingJob(SearchResult jobClicked)
        {
            // Load the current account
            Account account = Account.GetAccount(User.Identity.Name, RavenSession);
            if (account != null)
            {
                // Load the job
                var jobs = from j in account.Jobs
                           where j.URL == jobClicked.Url
                           select j;

                // Delete the job
                if (jobs != null && jobs.Count() > 0)
                {
                    Job job = jobs.First();
                    account.Jobs.Remove(job);
                    RavenSession.Store(account);
                }
            }

            return PartialView("JobNotSaved", jobClicked);
        }

        public PartialViewResult SaveBingJob(SearchResult jobClicked)
        {
            // Check if job was already saved
            // Load the current account
            Account account = Account.GetAccount(User.Identity.Name, RavenSession);
            if (account != null)
            {
                var jobs = from j in account.Jobs
                           where j.URL == jobClicked.Url
                           select j;

                if (jobs != null && jobs.Count() > 0)
                {
                    return PartialView("JobSaved", jobClicked);
                }

                // Create the job
                var job = new Job
                {
                    JobStatus = new JobStatus(),
                    ShortDescription = jobClicked.Description,
                    Title = jobClicked.Title,
                    URL = jobClicked.Url
                };
                job.JobStatus.Status = "Lead";

                if (account != null)
                {
                    if (account.Jobs == null)
                        account.Jobs = new List<Job>();

                    account.Jobs.Add(job);
                }
            }

            return PartialView("JobSaved", jobClicked);
        }

        public ActionResult Applied(string url)
        {
            var account = Account.GetAccount(User.Identity.Name, RavenSession);
            if (account != null)
            {
                var jobs = from j in account.Jobs
                          where j.URL == url
                          select j;

                if (jobs != null)
                {
                    Job job = jobs.First();
                    job.JobStatus.Status = "Applied";
                    RavenSession.Store(account);
                }
            }

            return View("Index", account.Jobs);
        }

        public ActionResult Current(string url)
        {
            var account = Account.GetAccount(User.Identity.Name, RavenSession);
            if (account != null)
            {
                var jobs = from j in account.Jobs
                           where j.URL == url
                           select j;

                if (jobs != null)
                {
                    Job job = jobs.First();
                    job.JobStatus.Status = "Current";
                    RavenSession.Store(account);
                }
            }

            return View("Index", account.Jobs);
        }    

        public ViewResult Details(string url)
        {
            return View(RavenSession.Load<Job>(url));
        }

        public ActionResult Delete(string url)
        {
            var account = Account.GetAccount(User.Identity.Name, RavenSession);
            Job jobToDelete = null;
            if (account != null)
            {
                var jobs = from j in account.Jobs
                           where j.URL == url
                           select j;

                if (jobs != null && jobs.Count() > 0)
                {
                    jobToDelete = jobs.First();
                    return View(jobToDelete);
                }
            }

            return View("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string url)
        {
            var account = Account.GetAccount(User.Identity.Name, RavenSession);
            if (account != null)
            {
                var jobs = from j in account.Jobs
                           where j.URL == url
                           select j;

                if (jobs != null && jobs.Count() > 0)
                {
                    Job job = jobs.First();
                    account.Jobs.Remove(job);
                    RavenSession.Store(account);
                }
            }

            return View("Index", account.Jobs);
        }
       
/*        
        //
        // GET: /Jobs/Edit/5
 
        public ActionResult Edit(int id)
        {
            Job job = db.Jobs.Find(id);
            ViewBag.Status = new SelectList(db.JobStatus1, "Id", "Status", job.Status);
            return View(job);
        }

        //
        // POST: /Jobs/Edit/5

        [HttpPost]
        public ActionResult Edit(Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Status = new SelectList(db.JobStatus1, "Id", "Status", job.Status);
            return View(job);
        }
        */
       
    }
}