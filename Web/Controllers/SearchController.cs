﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Model;

namespace Web.Controllers
{
    public class SearchController : BaseController
    {
        public ActionResult Index()
        {
            return View(RavenSession.Query<Job>());
        }

        public ActionResult Create(Job job)
        {
            RavenSession.Store(job);

            return RedirectToAction("Index");
        }

    }
}
