using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;

namespace Web.Controllers
{
    public class PricingController : BaseController
    {
        public PricingController(IDocumentSession documentSession)
            : base(documentSession)
        {
        }

        //
        // GET: /Pricing/

        public ActionResult Index()
        {
            return View();
        }

    }
}
