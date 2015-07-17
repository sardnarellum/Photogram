using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resources;

namespace Photogram.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = Localization.Portfolio;

            return /*Redirect("http://www.facebook.com/MullerAndrasPhotography");*/ View();
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