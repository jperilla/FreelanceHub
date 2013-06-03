using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Griffin.MvcContrib.RavenDb.Providers;
using Griffin.MvcContrib.Providers.Roles;
using System.Web.Mvc;

namespace Web.Attributes
{
    public class RavenDbAuthorizeAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (String.IsNullOrWhiteSpace(Roles))
                base.OnAuthorization(filterContext);

            IRoleRepository RoleRepository = new RavenDbAccountRepository(filterContext.HttpContext.Session);

            bool isInRole = false;
            foreach (String role in Roles.Split(','))
            {
                IEnumerable<string> usersInRole = RoleRepository.GetUsersInRole(null, role);
                if (usersInRole != null && usersInRole.Contains(filterContext.HttpContext.User.Identity.Name))
                {
                    isInRole = true;
                    break;
                }
            }

            if (!isInRole)
                base.OnAuthorization(filterContext);
        }
    }
}