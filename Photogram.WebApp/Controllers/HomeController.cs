using System;
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
            ViewBag.Title = Localization.Portfolio;

            return View(_db.Project.Include("ProjectInclude")
                .Where(x => x.Visible && x.ProjectInclude.Count() > 0)
                .ToArray());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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