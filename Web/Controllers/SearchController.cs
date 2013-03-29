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

        public ActionResult BingSearch(Search search)
        {
            // TODO: Get customer's sites and send them to search
            IList<string> sites = new List<string>();

            sites.Add("www.flexjobs.com/publicjobs/");
            BingSearchWeb bingSearch = new BingSearchWeb(sites);
            search.Results = bingSearch.Search(search.Query);
            return View("Index", search);
        }

       

    }
}
