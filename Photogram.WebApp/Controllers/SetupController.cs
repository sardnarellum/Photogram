using Photogram.WebApp.Models;
using System.Web.Mvc;

namespace Photogram.WebApp.Controllers
{
    [Authorize]
    public class SetupController : BaseController
    {
        // GET: Setup
        [ChildActionOnly]
        public ActionResult Index()
        {
            return PartialView("_SetupPartial", new SetupViewModel());
        }
    }
}