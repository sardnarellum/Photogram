using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Photogram.WebApp.Models;
using Resources;
using System.IO;
using System.Net;

namespace Photogram.WebApp.Controllers
{
    [Authorize]
    public class GalleryController : BaseController
    {
        //
        // GET: /Gallery/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Details(Int32? galleryId)
        {
            if (null == galleryId)
                return RedirectToAction("Index");

            var gallery = _db.Gallery.Include("Media").Include("TextValue").Where(x => x.Id == galleryId).FirstOrDefault();

            if (null == gallery)
                return RedirectToAction("Index");

            return View(gallery);
        }

        [ChildActionOnly]
        public ActionResult Add()
        {
            return PartialView("_AddGalleryPartial", new AddGalleryViewModel());
        }

        /// <summary>
        /// Add new gallery
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddGalleryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var gallery = new Gallery
                    {
                        Title = new TextValue[]
                        {
                            new TextValue
                            {
                                Text = model.Title,
                                Language = _db.Language.Where(x => x.Code == model.Language).FirstOrDefault()
                            }
                        },
                        Type = GalleryType.Portfolio,
                        Year = Int16.Parse(model.Year),
                        Visible = false,
                        Position = _db.Gallery.OrderByDescending(x => x.Position).FirstOrDefault().Position + 1,
                    };

                    if (!String.IsNullOrEmpty(model.Description))
                    {
                        gallery.Description = new TextValue[]
                        { 
                            new TextValue
                            {
                                Text = model.Description,
                                Language = _db.Language.Where(x => x.Code == model.Language).FirstOrDefault()
                            }
                        };
                    }

                    _db.Gallery.Add(gallery);
                    _db.SaveChanges();
                    ////ModelState.AddModelError("", WebResources.ErrGalleryInsertion);
                    model = new AddGalleryViewModel();
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("iop", ex.Message);
                return JsonView(ModelState.IsValid, "_AddGalleryPartial", model);
            }

            return JsonView(ModelState.IsValid, "_AddGalleryPartial", model);
        }

        /// <summary>
        /// Removes a gallery
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        [HttpGet] 
        public ActionResult Delete(Int32? galleryId)
        {
            //_db.Gallery.Remove(_db.Gallery.Where(x => x.Id == galleryId).First());

            if (null == galleryId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var gallery = _db.Gallery.Include("TextValue").Where(x => x.Id == galleryId).FirstOrDefault();

            if (null == gallery)
            {
                return new HttpNotFoundResult();
            }

            return View(gallery);
        }

        private JsonResult JsonView(bool success, string viewName, object model)
        {
            return Json(new { Success = success, View = RenderPartialView(viewName, model) });
        }

        private string RenderPartialView(string partialViewName, object model)
        {
            if (ControllerContext == null)
                return string.Empty;

            if (model == null)
                throw new ArgumentNullException("model");

            if (string.IsNullOrEmpty(partialViewName))
                throw new ArgumentNullException("partialViewName");

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, partialViewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ListPortfolio()
        {           
            return PartialView("_GalleryListPortfolioPartial",
                _db.Gallery.Where(x => x.Type == GalleryType.Portfolio).ToArray());
        }
	}
}