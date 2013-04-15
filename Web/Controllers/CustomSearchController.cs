using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Raven.Client;
using Raven.Abstractions.Data;
using Raven.Abstractions.Smuggler;

namespace Web.Controllers
{
    [Authorize]
    public class CustomSearchController : BaseController
    {
        public CustomSearchController(IDocumentSession documentSession)
            : base(documentSession)
        {
        }

        //
        // GET: /CustomSearch/
        public ActionResult Index()
        {
            // Get all the sites
            IList<CustomSearchSite> sites = (from s in RavenSession.Query<CustomSearchSite>()
                                                select s).ToList();

            if(sites == null)
                sites = new List<CustomSearchSite>();

            return View(sites);
        }

        //
        // GET: /CustomSearch/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CustomSearch/Create

        [HttpPost]
        public ActionResult Create(CustomSearchSite site)
        {
            if (ModelState.IsValid)
            {
                // Add site
                RavenSession.Store(site);
                return RedirectToAction("Index");
            }

            return View(site);
        }

        //
        // GET: /CustomSearch/Edit/elance

        public ActionResult Edit(int id)
        {
            var siteToEdit = RavenSession.Load<CustomSearchSite>("CustomSearchSites/" + id);

            if (siteToEdit == null)
                return HttpNotFound();

            return View(siteToEdit);
        }

        //
        // POST: /CustomSearch/Edit/elance

        [HttpPost]
        public ActionResult Edit(CustomSearchSite site)
        {
            if (ModelState.IsValid)
            {
                CustomSearchSite siteToEdit = RavenSession.Load<CustomSearchSite>("CustomSearchSites/" + site.Id);
                siteToEdit.DisplayName = site.DisplayName;
                siteToEdit.JobType = site.JobType;
                siteToEdit.Url = site.Url;
                
                return RedirectToAction("Index");
            }

            return View(site);
        }

        //
        // GET: /CustomSearch/Delete/elance

        public ActionResult Delete(int id)
        {
            var siteToDelete = RavenSession.Load<CustomSearchSite>("CustomSearchSites/" + id);

            if (siteToDelete == null)
                return HttpNotFound();

            return View(siteToDelete);
        }

        //
        // POST: /CustomSearch/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var siteToDelete = RavenSession.Load<CustomSearchSite>("CustomSearchSites/" + id);

            if (siteToDelete == null)
                return HttpNotFound();
            else
                RavenSession.Delete(siteToDelete);
    
            return RedirectToAction("Index");
        }


        //
        // GET: /CustomSearch/UserView
        public ActionResult UserView()
        {
            if (User.Identity.IsAuthenticated)
            {
                // See if user customized search yet, if so populate their previous selections
                // otherwise pre-populate based on their job types - suggested search
                Account account = Account.GetAccount(User.Identity.Name, RavenSession);
                if (account != null)
                {
                    if (account.SitesToSearch == null)
                        account.SitesToSearch = new List<string>();

                    IList<CustomSearchSite> sites = (from site in RavenSession.Query<CustomSearchSite>()
                                                     select site).ToList();

                    if (sites == null)
                        sites = new List<CustomSearchSite>();
                    
                    foreach (var s in sites)
                    {
                        if (account.SitesToSearch.Contains(s.Id))
                            s.UserWantsToSearch = true;
                    }

                    return View(sites);
                }
                else
                {
                    // TOOD: prepopulate based on job type
                }

            }

            return RedirectToAction("Index", "Search");
        }

        [HttpPost]
        public ActionResult UserView(IList<CustomSearchSite> userSites)
        {
            if (User.Identity.IsAuthenticated)
            {
                Account account = Account.GetAccount(User.Identity.Name, RavenSession);
                if (account != null)
                {
                    var sitesToSearch = from site in userSites
                                        where site.UserWantsToSearch == true
                                        select site;

                    var sitesNotToSearch = from site in userSites
                                           where site.UserWantsToSearch == false
                                           select site;

                    // Add sites to search
                    foreach (var s in sitesToSearch)
                    {
                        if (!account.SitesToSearch.Contains(s.Id))
                            account.SitesToSearch.Add(s.Id);
                    }

                    // Remove sites to not search
                    foreach (var s in sitesNotToSearch)
                    {
                        if (account.SitesToSearch.Contains(s.Id))
                            account.SitesToSearch.Remove(s.Id);
                    }

                    // Generate the Google Custom Search cse file for this user, must do this after the Sites to search are set
                    account.GenerateCseFile(RavenSession);
                    

                    ViewBag.Comment = "Saved";
                    if (Request.IsAjaxRequest())
                        return Json("success");
                    else
                        return View(userSites);
                }
            }


            ViewBag.Comment = "Oops! We had trouble saving, try again.";
            return View(userSites);
        }

    }
}
