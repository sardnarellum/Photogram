using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Photogram.WebApp.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }

    [Authorize]
    public class TemplatesController : BaseController
    {
        public ActionResult Blog()
        {
            return PartialView();
        }

        public ActionResult BlogPost()
        {
            return PartialView();
        }

        public ActionResult Dashboard()
        {
            return PartialView();
        }

    }
}