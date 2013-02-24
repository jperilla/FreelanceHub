using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Raven.Client;

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
            return View();
        }

    }
}
