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
            return RedirectToAction("Index", "Project"); // Under Dev.
        }

        public async Task<ActionResult> BlogPostListAsync()
        {
            return PartialView();
        }
    }
}