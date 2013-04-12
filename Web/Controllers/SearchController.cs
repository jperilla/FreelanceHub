using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Raven.Client;
using System.Net;
using BingSearchAPI;

namespace Web.Controllers
{
    [Authorize]
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

        public ActionResult Customize()
        {
            return View();
        }

        public ActionResult RedirectDisclaimer(SearchResult result)
        {
            return View("RedirectDisclaimer", result);
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
                search.FirstVisit = false;
                return View("Index", search);
            }

            return View("LandingPage", "Index");
        }

       

    }
}
