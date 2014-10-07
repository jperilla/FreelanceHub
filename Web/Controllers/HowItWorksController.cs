using System.Web.Mvc;
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
            return View();
        }
    }
}
