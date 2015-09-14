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
        public ActionResult Index()
        {
            var items = _db.Project.Include("Title").OrderBy(x => x.Position).ToList();

            return View(items);
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

                projectTitle = project.CurrentTitleText();
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

            var project = _db.Project.Include("ProjectInclude").
                                    Include("Title").
                                    Include("Description").
                                    Where(x => x.Id == projectId).FirstOrDefault();

            if (null == project)
                return RedirectToAction("Index", "Home");

            ViewBag.Title = project.CurrentTitleText();

            return View("Details", project);
        }

        [HttpGet]
        public ActionResult Edit(int? projectId)
        {
            if (null == projectId)
                return RedirectToAction("Index", "Home");

            var project = _db.Project.Include("ProjectInclude")
                .Include("Title").Include("Description")
                .Where(x => x.Id == projectId).FirstOrDefault();

            if (null == project)
                return RedirectToAction("Index", "Home");

            var currLang = _db.Language
                .Where(x => x.LCID == CultureInfo.CurrentCulture.LCID)
                .FirstOrDefault();

            if (null != currLang)
                currLang = _db.Language.Where(x => x.LCID == 1033)
                    .FirstOrDefault();

            var properties = new ProjectProperties
            {
                Id = project.Id,
                Title = project.CurrentTitleText(),
                Description = project.CurrentDescriptionText(),
                LCID = currLang.LCID,
                Year = project.Year.ToString(),
                Visible = project.Visible,
                Slug = project.Slug,
            };

            var model = new ProjectViewModel
            {
                Properties = properties
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectProperties viewModel)
        {
            var project = _db.Project.Where(x => x.Id == viewModel.Id)
                .FirstOrDefault();

            if (null == project)
            {
                ModelState.AddModelError("unknown project",
                    Localization.ErrInvalidProject);
            }

            if (_db.Project.Any(
                    x => x.Slug == viewModel.Slug && x.Id != viewModel.Id)
                   )
            {
                ModelState.AddModelError("slug occupied",
                    Localization.ErrOccupiedSlug);
            }

            if (ModelState.IsValid)
            {
                var language = _db.Language
                    .Where(x => x.LCID == viewModel.LCID).FirstOrDefault();


                var title = project.Title.Where(x => x.Language == language)
                    .FirstOrDefault();

                if (null == title)
                {
                    title = new ProjectTitle
                    {
                        Language = language,
                        Text = viewModel.Title
                    };

                    project.Title.Add(title);
                }
                else
                {
                    title.Text = viewModel.Title;
                }


                var description = project.Description
                    .Where(x => x.Language == language).FirstOrDefault();

                if (!string.IsNullOrEmpty(viewModel.Description))
                {
                    if (null == description)
                    {
                        description = new ProjectDescription
                        {
                            Language = language,
                            Text = viewModel.Description
                        };

                        project.Description.Add(description);
                    }
                    else
                    {
                        description.Text = viewModel.Description;
                    }
                }
                else if (null != description)
                {
                    _db.Translation.Remove(description);
                }


                project.Slug = viewModel.Slug;
                project.Year = short.Parse(viewModel.Year);
                project.Visible = viewModel.Visible;

                _db.SaveChanges();
            }

            return PartialView("_EditPropertiesPartial", viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Add()
        {
            return PartialView("_AddProjectPartial", new ProjectProperties());
        }

        /// <summary>
        /// Add new project
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProjectProperties model)
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

                    var project = new Project
                    {
                        Type = ProjectType.Portfolio,
                        Year = short.Parse(model.Year),
                        Visible = false,
                        Position = _db.Project.Count() != 0
                            ? _db.Project.OrderByDescending(x => x.Position).FirstOrDefault().Position + 1
                            : 1
                    };

                    project.Title = new ProjectTitle[]
                    {
                        new ProjectTitle
                        {
                            Language = language,
                            Text = model.Title
                        }
                    };

                    if (!string.IsNullOrEmpty(model.Description))
                    {
                        project.Description = new ProjectDescription[]
                        {
                            new ProjectDescription
                            {
                                Language = language,
                                Text = model.Description
                            }
                        };
                    }

                    project.Slug = model.Title.ToLower().Replace(" ", "-");

                    _db.Project.Add(project);
                    _db.SaveChanges();
                    ////ModelState.AddModelError("", Localization.ErrProjectInsertion);
                    model = new ProjectProperties();
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

            var gallery = _db.Project.Where(x => x.Id == galleryId)
                .FirstOrDefault();

            if (null == gallery)
            {
                return new HttpNotFoundResult();
            }

            gallery.Visible = gallery.Visible ? false : true;

            _db.SaveChanges();

            return PartialView("_WorksDropDownPartial");
        }

        [HttpGet]
        [AjaxErrorHandler]
        public JsonResult SetPosition(int? projectId, int? position)
        {
            if (null == projectId)
                throw new ArgumentNullException("projectId",
                    Localization.ErrArgNull);

            var project = _db.Project.Where(x => x.Id == projectId)
                .FirstOrDefault();

            if (null == project)
                throw new ArgumentException(
                    projectId.ToString() + " does not exists in Project.",
                    "projectId");

            if (null == position)
                throw new ArgumentNullException("position",
                    Localization.ErrArgNull);

            project.SetPosition((int)position);

            _db.SaveChanges();

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Up(Int32? galleryId)
        {
            if (null == galleryId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var gallery = _db.Project.Where(x => x.Id == galleryId)
                .FirstOrDefault();

            if (null == gallery)
            {
                return new HttpNotFoundResult();
            }

            var gallery2 = _db.Project
                .Where(x => x.Position - 1 == gallery.Position)
                .FirstOrDefault();

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

            var project = _db.Project.Where(x => x.Id == projectId)
                .FirstOrDefault();

            if (null == project)
            {
                return new HttpNotFoundResult();
            }

            var project2 = _db.Project
                .Where(x => x.Position + 1 == project.Position)
                .FirstOrDefault();

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
        public ActionResult Delete(int? projectId)
        {
            if (null == projectId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = _db.Project.Include("Title").Include("Description")
                .Where(x => x.Id == projectId).FirstOrDefault();

            if (null == project)
            {
                return new HttpNotFoundResult();
            }

            _db.Project.Remove(project);

            var sortables = _db.Project.Where(x => x.Position > project.Position);

            foreach (var elem in sortables)
            {
                --elem.Position;
            }
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
	}
}