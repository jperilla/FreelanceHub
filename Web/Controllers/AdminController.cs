using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;

namespace Web.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(IDocumentSession documentSession)
            : base(documentSession)
        {
        }

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

    }
}
