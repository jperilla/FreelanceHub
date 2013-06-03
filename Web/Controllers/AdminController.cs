using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;
using Griffin.MvcContrib.Providers.Roles;

namespace Web.Controllers
{   
    [Authorize(Roles="Administrator")]
    public class AdminController : BaseController
    {

        public AdminController(IRoleRepository roleRepository, IDocumentSession documentSession)
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
