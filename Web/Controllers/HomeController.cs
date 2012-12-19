
using System.Web.Mvc;
using Web.Model;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        public ActionResult Index(Account account)
        {
            return View("_Home", account);
        }

    }
}
