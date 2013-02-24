using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Raven.Client;

namespace Web.Controllers
{
    public class HowItWorksController : BaseController
    {
        public HowItWorksController(IDocumentSession documentSession)
            : base(documentSession)
        {
        }

        //
        // GET: /HowItWorks/
        public ActionResult Index()
        {
            throw new Exception("Test exception");
               // return View();
        }
    }
}
