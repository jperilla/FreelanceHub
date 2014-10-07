using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Models;
using Raven.Client;
using BingSearchAPI;

namespace Web.Controllers
{
    [Authorize(Roles = "Basic, Agency, FullTime, PartTime, Administrator")]
    public class SearchController : BaseController
    {
        public SearchController(IDocumentSession documentSession)
            : base(documentSession)
        {
        }

        public ActionResult Index()
        {
            Search search = new Search();
            return View(search);
        }

        public ActionResult GoogleSearch()
        {
            var account = Account.GetAccount(User.Identity.Name, RavenSession);
            return View(account);
        }

        public ActionResult GoogleSearchResults()
        {
            var account = Account.GetAccount(User.Identity.Name, RavenSession);
            return View("GoogleSearch", account);
        }

        public ActionResult Customize()
        {
            return View();
        }

        public ActionResult SaveSearchQuery(String queryString)
        {
            Account account = Account.GetAccount(User.Identity.Name, RavenSession);
            if (account != null)
            {
                var searches = from s in account.Searches
                               where s == queryString
                           select s;

                if (searches != null && searches.Count() > 0)
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }

                // Save the search query              
                if (account.Searches == null)
                    account.Searches = new List<String>();

                account.Searches.Add(queryString);
                RavenSession.Store(account);
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RedirectDisclaimerUrl(string url)
        {
            return View("RedirectDisclaimerUrl", null, url);
        }

        public ActionResult RedirectDisclaimer(SearchResult result)
        {            
            return View("RedirectDisclaimer", result);
        }

        public ActionResult RedirectDisclaimerJob(Job job)
        {
            return View("RedirectDisclaimerJob", job);
        }


        public ActionResult BingSearch(Search search)
        {
            if (User.Identity.IsAuthenticated)
            {
                IList<string> sites = new List<string>();

                // Get user saved sites
                Account account = Account.GetAccount(User.Identity.Name, RavenSession);
                foreach(var customerSite in account.SitesToSearch)
                {
                    CustomSearchSite siteToSearch = RavenSession.Load<CustomSearchSite>(customerSite);
                    if(siteToSearch != null)
                        sites.Add(siteToSearch.Url);                
                }

                // Search bing
                BingSearchWeb bingSearch = new BingSearchWeb(sites);
                search.Query = "intitle:" + search.Query + " inbody:" + search.Query;
                List<Bing.WebResult> webResults = bingSearch.Search(search.Query).ToList();
                search.Results = webResults.ConvertAll(SearchResult.BingResultToSearchResult);

                // Set isSaved on each result
                foreach(var result in search.Results)
                {
                    var jobs = from j in account.Jobs
                           where j.URL == result.Url
                           select j;

                    if ((jobs != null) && (jobs.Count() > 0))
                    {
                        result.IsSaved = true;
                    }
                }

                search.FirstVisit = false;
                return View("Index", search);
            }

            return View("LandingPage", "Index");
        }

       

    }
}
