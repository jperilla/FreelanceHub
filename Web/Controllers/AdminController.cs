using System.Web.Mvc;
using Raven.Client;
using Griffin.MvcContrib.Providers.Roles;

namespace Web.Controllers
{   
    [Authorize(Users="julie.perilla@gmail.com")]
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
