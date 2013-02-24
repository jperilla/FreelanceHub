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
        HowItWorksController(IDocumentSession documentSession)
            : base(documentSession)
        {
        }

        //
        // GET: /HowItWorks/
        public ActionResult Index()
        {            
                return View();
        }
    }
}
