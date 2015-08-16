using Photogram.WebApp.Models;
using Resources;
using System;
using System.Globalization;
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
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [AjaxErrorHandler]
        public JsonResult ProjectName(int? projectId)
        {
            if (null == projectId)
                throw new ArgumentNullException("projectId",
                    Localization.ErrArgNull);

            string projectTitle;

            if (-1 != projectId)
            {
                var project = _db.Project.Where(x => x.Id == projectId)
                    .FirstOrDefault();

                if (null == project)
                    throw new ArgumentException(
                        projectId.ToString() + " does not exists in Project.",
                        "mediaId");

                var language = _db.Language
                    .Where(x => x.LCID == CultureInfo.CurrentCulture.LCID)
                    .FirstOrDefault();

                // If CurrentCulture is set to unknown default language is English
                if (null == language) 
                {
                    language = _db.Language.Where(x => x.LCID == 1033)
                        .FirstOrDefault();
                }

                var title = project.Title
                        .Where(x => x.Language == language)
                        .FirstOrDefault();

                if (null != title)
                {
                    projectTitle = title.Text;
                }
                else // If title does not exists with current lang or english or 1033 is deleted from db.
                {
                    title = project.Title.FirstOrDefault();
                    if (null != title)
                    {
                        projectTitle = title.Text;
                    }
                    else
                    {
                        projectTitle = "";
                    }
                }
            }
            else
            {
                projectTitle = Localization.NoProject;
            }

            return Json(new { ProjectTitle = projectTitle },
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
                    var language = _db.Language
                        .Where(x => x.LCID == model.LCID).FirstOrDefault();

                    if (null == language)
                    {
                        ModelState.AddModelError("invalid_lang",
                            Localization.ErrInvalidLanguage);
                    }

                    var gallery = new Project
                    {
                        Type = ProjectType.Portfolio,
                        Year = short.Parse(model.Year),
                        Visible = false,
                        Position = _db.Project.Count() != 0
                            ? _db.Project.OrderByDescending(x => x.Position).FirstOrDefault().Position + 1
                            : 1
                    };

                    gallery.Title = new ProjectTitle[]
                    {
                        new ProjectTitle
                        {
                            Language = language,
                            Text = model.Title
                        }
                    };

                    if (!string.IsNullOrEmpty(model.Description))
                    {
                        gallery.Description = new ProjectDescription[]
                        {
                            new ProjectDescription
                            {
                                Language = language,
                                Text = model.Description
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