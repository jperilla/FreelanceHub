using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Griffin.MvcContrib.RavenDb.Providers;
using Griffin.MvcContrib.Providers.Roles;
using Raven.Client;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles="Administrator")]
    public class RolesController : BaseController
    {
        public IRoleRepository RoleRepository { get; private set; }

        public RolesController(IDocumentSession documentSession, IRoleRepository roleRepository)
            : base(documentSession)
        {
            RoleRepository = roleRepository;
        }


        //
        // GET: /Roles/

        public ActionResult Index()
        {
            // Get all the roles
            IEnumerable<String> roles = RoleRepository.GetRoleNames(null);

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
                RoleRepository.CreateRole(null, role.Name);
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
                RoleRepository.RemoveRole(null, roleName);

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
                RoleRepository.AddUserToRole(null, userRole.Role, userRole.Username);
                return View("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
