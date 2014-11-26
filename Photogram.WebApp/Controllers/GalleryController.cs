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
                        Position = _db.Gallery.Count() != 0
                            ? _db.Gallery.OrderByDescending(x => x.Position).FirstOrDefault().Position + 1
                            : 1,
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

            return JsonView(ModelState.IsValid, "_AddGalleryPartial", model,
                "_WorksDropDownPartial", model); // A másodiknál nem kellene a model
        }

        public ActionResult SetVisibility(Int32? galleryId)
        {
            if (null == galleryId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var gallery = _db.Gallery.Where(x => x.Id == galleryId).FirstOrDefault();

            if (null == gallery)
            {
                return new HttpNotFoundResult();
            }

            gallery.Visible = gallery.Visible ? false : true;

            _db.SaveChanges();

            return View();
        }

        public ActionResult Up(Int32? galleryId)
        {
            if (null == galleryId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var gallery = _db.Gallery.Where(x => x.Id == galleryId).FirstOrDefault();

            if (null == gallery)
            {
                return new HttpNotFoundResult();
            }

            var gallery2 = _db.Gallery.Where(x => x.Position - 1 == gallery.Position).FirstOrDefault();

            if (null != gallery2)
            {
                --gallery2.Position;
                ++gallery.Position;

                _db.SaveChanges();
            }

            return View();
        }

        public ActionResult Down(Int32? galleryId)
        {
            if (null == galleryId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var gallery = _db.Gallery.Where(x => x.Id == galleryId).FirstOrDefault();

            if (null == gallery)
            {
                return new HttpNotFoundResult();
            }

            var gallery2 = _db.Gallery.Where(x => x.Position + 1 == gallery.Position).FirstOrDefault();

            if (null != gallery2)
            {
                ++gallery2.Position;
                --gallery.Position;

                _db.SaveChanges();
            }

            return View();
        }

        /// <summary>
        /// Removes a gallery
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        [HttpGet] 
        public ActionResult Delete(Int32? galleryId)
        {
            if (null == galleryId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var gallery = _db.Gallery.Include("Title").Include("Description").Where(x => x.Id == galleryId).FirstOrDefault();

            if (null == gallery)
            {
                return new HttpNotFoundResult();
            }

            _db.Gallery.Remove(gallery);
            _db.SaveChanges();

            return View();
        }

        private JsonResult JsonView(bool success, string viewName, object model)
        {
            return Json(new { Success = success, View = RenderPartialView(viewName, model) });
        }

        private JsonResult JsonView(bool success, string viewName1, object model1, string viewName2, object model2)
        {
            return Json(new { Success = success,
                View1 = RenderPartialView(viewName1, model1), View2 = RenderPartialView(viewName2, model2)
            });
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
                _db.Gallery.OrderByDescending(x => x.Position).Where(x => x.Type == GalleryType.Portfolio).ToArray());
        }
	}
}