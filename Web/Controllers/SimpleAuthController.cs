using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SimpleSocialAuth.MVC3;
using Web.Model;

namespace Web.Controllers
{
  public class SimpleAuthController : BaseController
  {
    public ActionResult LogIn()
    {
      Session["ReturnUrl"] =
        Request.QueryString["returnUrl"];

      return View();
    }

    // TODO: HI Errors handling
    [HttpPost]
    public ActionResult Authenticate(AuthType authType)
    {
      var authHandler =
        AuthHandlerFactory.CreateAuthHandler(authType);

      string redirectUrl =
        authHandler
          .PrepareAuthRequest(
            Request,
            Url.Action("DoAuth", new { authType = (int)authType }));

      return
        Redirect(redirectUrl);
    }

    public ActionResult DoAuth(AuthType authType)
    {
      var authHandler =
        AuthHandlerFactory.CreateAuthHandler(authType);

      // TODO: HI think of if ids are unique among differenct providers
      var userData = 
        authHandler
          .ProcessAuthRequest(Request as HttpRequestWrapper);

      if (userData == null)
      {
        TempData["authError"] =
          "Authentication has failed.";

        return
          RedirectToAction("LogIn");
      }

      FormsAuthentication.SetAuthCookie(userData.UserName, true);
        
      // Load user account
      Account authenticatedAccount = RavenSession.Load<Account>(userData.Email);
      if (null != authenticatedAccount)
      {
          // Check if subscription is current, if not redirect to account page
          // for user to update their subscription
          if (!authenticatedAccount.IsAccountCurrent())
          {
              return RedirectToAction("AccountSuspended", "Account");
          }
      }

      return 
        Session["ReturnUrl"] != null
        ? (ActionResult) Redirect((string) Session["ReturnUrl"])
        : RedirectToAction("Index", "Home", authenticatedAccount);
    }
  }
}