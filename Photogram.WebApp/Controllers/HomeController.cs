﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resources;
using Photogram.WebApp.Models;

namespace Photogram.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var setup = _db.Setup.FirstOrDefault();
            ViewBag.MainTitle = setup != null
                ? setup.CurrentMainTitleText()
                : Localization.PhotogramNet;
            ViewBag.Footer = setup != null ? setup.CurrentFooterText() : "";
            ViewBag.Title = Localization.Portfolio;

            return View(_db.Project.Include("ProjectInclude")
                .OrderBy(x => x.Position)
                .Where(x => x.Visible && x.ProjectInclude.Count() > 0)
                .ToArray());
        }

        [ChildActionOnly]
        public ActionResult About()
        {
            var setup = _db.Setup.FirstOrDefault();

            if (null == setup)
            {
                return View(new AboutModel());
            }


            var model = new AboutModel
            {
                Lead = setup.CurrentAboutLeadText(),
                Body = setup.CurrentAboutBodyText()
            };

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult Contact()
        {
            var setup = _db.Setup.FirstOrDefault();

            if (null == setup)
            {
                return View(new ContactModel());
            }

            var model = new ContactModel
            {
                Lead = setup.CurrentContactLeadText(),
                Email = setup.Email,
                Phone = setup.Phone,
                FacebookURL = setup.FacebookURL,
                GitHubURL = setup.GitHubURL,
                InstagramURL = setup.InstagramURL,
                LinkedInURL = setup.LinkedInURL
            };

            return View(model);
        }

        public ActionResult Redirects()
        {
            return View();
        }

        
        public ActionResult WorksDropDown()
        {
            return PartialView("_WorksDropDownPartial");
        }
    }
}