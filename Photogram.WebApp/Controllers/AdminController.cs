using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Photogram.WebApp.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Galleries()
        {
            return View(_db.Gallery.ToList());
        }

        public ActionResult GalleryDetails(Int32? galleryId)
        {
            if (null == galleryId)
                RedirectToAction("Galleries");

            var gallery = _db.Gallery.Include("Media").Where(x => x.Id == galleryId).FirstOrDefault();

            if (null == gallery)
                RedirectToAction("Galleries");

            return View("GalleryDetails", gallery);
        }
	}
}