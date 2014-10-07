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
