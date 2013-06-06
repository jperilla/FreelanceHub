using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Griffin.MvcContrib.RavenDb.Providers;
using Griffin.MvcContrib.Providers.Roles;
using Raven.Client;
using Web.Models;
using System.Web.Security;
using Web.Attribute;

namespace Web.Controllers
{
    [Authorize(Users = "julie.perilla@gmail.com")]
    public class RolesController : BaseController
    {
        public RolesController(IDocumentSession documentSession)
            : base(documentSession)
        {
        }


        //
        // GET: /Roles/

        public ActionResult Index()
        {
            // Get all the roles
            IEnumerable<String> roles = Roles.GetAllRoles();

            if (roles == null)
                roles = new List<String>();

            return View(roles);
        }

        //
        // GET: /Roles/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create

        [HttpPost]
        public ActionResult Create(Role role)
        {
            try
            {
                Roles.CreateRole(role.Name);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Roles/Delete/5

        public ActionResult Delete(string roleName)
        {
            return View(roleName);
        }

        //
        // POST: /Roles/Delete/5

        [HttpPost]
        public ActionResult Delete(string roleName, FormCollection collection)
        {
            try
            {
                Roles.DeleteRole(roleName);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddUserToRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUserToRole(UserRole userRole)
        {
            try
            {
                Roles.AddUserToRole(userRole.Username, userRole.Role);
                return View("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
