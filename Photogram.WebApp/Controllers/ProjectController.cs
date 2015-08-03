using Photogram.WebApp.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Photogram.WebApp.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ListPortfolio()
        {
            return PartialView("_ProjectListPortfolioPartial",
                _db.Project.OrderByDescending(x => x.Position)
                .Where(x => x.Type == ProjectType.Portfolio).ToArray());
        }

        /// <summary>
        /// TODO: error exception handling
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public JsonResult ProjectName(int? projectId)
        {
            if (null == projectId)
                return Json(new { Success = false, Message = "" }); // TODO: error msgs

            var project = _db.Project.Where(x => x.Id == projectId)
                .FirstOrDefault();

            if (null == project)
                return Json(new { Success = false, Message = "" }); // TODO: error msgs

            return Json(new { ProjectTitle = project.Title.FirstOrDefault().Text }, // TODO: ML support
                JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Details(Int32? projectId)
        {
            if (null == projectId)
                return RedirectToAction("Index", "Home");

            var project = _db.Project.Include("Media").
                                    Include("Title").
                                    Include("Description").
                                    Where(x => x.Id == projectId).FirstOrDefault();

            if (null == project)
                return RedirectToAction("Index", "Home");

            ViewBag.Title = project.Title.FirstOrDefault().Text; // TODO: ML support

            return View("Details", project);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Add()
        {
            return PartialView("_AddProjectPartial", new AddProjectViewModel());
        }

        /// <summary>
        /// Add new project
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddProjectViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var gallery = new Project
                    {
                        Title = new TextValue[]
                        {
                            new TextValue
                            {
                                Text = model.Title,
                                Language = _db.Language.Where(x => x.Code == model.Language).FirstOrDefault()
                            }
                        },
                        Type = ProjectType.Portfolio,
                        Year = Int16.Parse(model.Year),
                        Visible = false,
                        Position = _db.Project.Count() != 0
                            ? _db.Project.OrderByDescending(x => x.Position).FirstOrDefault().Position + 1
                            : 1
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

                    _db.Project.Add(gallery);
                    _db.SaveChanges();
                    ////ModelState.AddModelError("", Localization.ErrProjectInsertion);
                    model = new AddProjectViewModel();
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("iop", ex.Message);
                return JsonView(ModelState.IsValid, "_AddProjectPartial", model);
            }

            return JsonView(ModelState.IsValid, "_AddProjectPartial", model,
                "_WorksDropDownPartial", model); // A másodiknál nem kellene a model
        }

        [HttpGet]
        public ActionResult SetVisibility(Int32? galleryId)
        {
            if (null == galleryId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var gallery = _db.Project.Where(x => x.Id == galleryId).FirstOrDefault();

            if (null == gallery)
            {
                return new HttpNotFoundResult();
            }

            gallery.Visible = gallery.Visible ? false : true;

            _db.SaveChanges();

            return PartialView("_WorksDropDownPartial");
        }

        [HttpGet]
        public ActionResult Up(Int32? galleryId)
        {
            if (null == galleryId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var gallery = _db.Project.Where(x => x.Id == galleryId).FirstOrDefault();

            if (null == gallery)
            {
                return new HttpNotFoundResult();
            }

            var gallery2 = _db.Project.Where(x => x.Position - 1 == gallery.Position).FirstOrDefault();

            if (null != gallery2)
            {
                --gallery2.Position;
                ++gallery.Position;

                _db.SaveChanges();
            }

            return PartialView("_WorksDropDownPartial");
        }

        [HttpGet]
        public ActionResult Down(Int32? projectId)
        {
            if (null == projectId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = _db.Project.Where(x => x.Id == projectId).FirstOrDefault();

            if (null == project)
            {
                return new HttpNotFoundResult();
            }

            var project2 = _db.Project.Where(x => x.Position + 1 == project.Position).FirstOrDefault();

            if (null != project2)
            {
                ++project2.Position;
                --project.Position;

                _db.SaveChanges();
            }

            return PartialView("_WorksDropDownPartial");
        }

        /// <summary>
        /// Removes a project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(Int32? projectId)
        {
            if (null == projectId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = _db.Project.Include("Title").Include("Description").Where(x => x.Id == projectId).FirstOrDefault();

            if (null == project)
            {
                return new HttpNotFoundResult();
            }

            _db.Project.Remove(project);
            _db.SaveChanges();

            return PartialView("_WorksDropDownPartial");
        }
	}
}