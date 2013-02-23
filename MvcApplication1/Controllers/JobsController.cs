using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Model;

namespace Web.Controllers
{ 
    public class JobsController : BaseController
    {

        // 
        // GET: /Jobs/

        public ViewResult Index()
        {
            return View(RavenSession.Query<Job>());
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveFavorite(Job jobClicked)
        {
            try
            {
                var job = new Job
                    {
                        JobStatus = new JobStatus(),
                        ShortDescription = jobClicked.ShortDescription,
                        Title = jobClicked.Title,
                        URL = jobClicked.URL,
                        Budget = "$100"
                    };
                job.JobStatus.Status = "Lead";

                RavenSession.Store(job);
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error");
            }
        }

        public ActionResult Applied(int id)
        {
            Job job = RavenSession.Load<Job>(id);
            job.JobStatus.Status = "Applied";
            RavenSession.Store(job);
            return View("Index", RavenSession.Query<Job>());
        }

        public ActionResult Current(int id)
        {
            Job job = RavenSession.Load<Job>(id);
            job.JobStatus.Status = "Current";
            RavenSession.Store(job);
            return View("Index", RavenSession.Query<Job>());
        }    

        public ViewResult Details(int id)
        {
            return View(RavenSession.Load<Job>(id));
        }

        public ActionResult Delete(int id)
        {
            return View(RavenSession.Load<Job>(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Job job = RavenSession.Load<Job>(id);
            RavenSession.Delete<Job>(job);
            return RedirectToAction("Index");
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