using System.Web.Mvc;
using Web.Model;
using System.Web.Security;

namespace Web.Controllers
{
    public class LandingPageController : BaseController
    {
        //
        // GET: /Index/
        public ActionResult Index()
        {
            return View();
        }
    }
}
