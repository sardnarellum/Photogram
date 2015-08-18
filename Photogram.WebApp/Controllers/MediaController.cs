using Photogram.WebApp.Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Photogram.WebApp.Controllers
{
    [Authorize]
    public class MediaController : BaseController
    {
        public MediaController()
        {
            ViewBag.Title = Localization.ContentMgmt;
        }

        public ActionResult Index()
        {
            var model = new ContentMgmtViewModel
            {
                Medias = _db.Media.ToList(),
                Projects = _db.Project.OrderByDescending(x => x.Position)
                    .Where(x => x.Type == ProjectType.Portfolio).ToList()
            };

            return View("Index", model);
        }

        [ChildActionOnly]
        public ActionResult Thumbnail(int? mediaId)
        {
            if (null == mediaId)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var item = _db.Media
                .Include("Title").Include("Description").Include("Project")
                .Where(x => x.Id == mediaId).FirstOrDefault();

            if (null == item)
                return new HttpNotFoundResult();

            return PartialView(item);
        }

        [AjaxErrorHandler]
        public JsonResult DeleteJson(int? mediaId)
        {
            if (null == mediaId)
                throw new ArgumentNullException("mediaId",
                    Localization.ErrArgNull);

            if (!Delete((int)mediaId))
                throw new ArgumentException(
                    mediaId.ToString() + " does not exists in Media.",
                    "mediaId");

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxErrorHandler]
        public JsonResult AjaxUpload()
        {
            string fileName = "";
            var fileId = -1;

            foreach (string e in Request.Files)
            {
                var file = Request.Files[e];
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentType.Contains("image"))
                    {
                        fileName = GetUniqueFileName()
                            + Path.GetExtension(file.FileName);
                        SaveFile(file, fileName,
                            ServerDirectory(Macros.UploadPathImg));

                        var media = new Media
                        {
                            FileName = fileName,
                            Type = MediaType.Image //TODO: another file types
                        };

                        _db.Media.Add(media);
                        _db.SaveChanges();

                        fileId = _db.Media.Where(x => x.FileName == fileName)
                            .FirstOrDefault().Id;
                    }
                    else
                    {
                        throw 
                            new ArgumentException("Only image files accepted.",
                            "file");
                    }
                }
            }

            return Json(new { FileName = fileName, FileId = fileId });
        }

        /// <summary>
        /// Action for displaying editing form with current data.
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxErrorHandler]
        public JsonResult InitEdit(int? mediaId)
        {
            if (null == mediaId)
                throw new ArgumentNullException("mediaId",
                    Localization.ErrArgNull);

            var media = _db.Media.Include("Title").Include("Description")
                .Where(x => x.Id == mediaId).FirstOrDefault();

            if (null == media)
                throw new ArgumentException(
                    mediaId.ToString() + " does not exists in Media.",
                    "mediaId");

            var currLCID = CultureInfo.CurrentCulture.LCID;

            var currTitle = media.Title
                    .Where(x => x.Language.LCID == currLCID)
                    .FirstOrDefault();

            var currDesc = media.Description
                    .Where(x => x.Language.LCID == currLCID)
                    .FirstOrDefault();

            var model = new MediaInformation {
                FileName = media.FileName,
                MediaId = media.Id,
                LCID = _db.Language.Where(x => x.LCID == currLCID)
                    .FirstOrDefault().LCID,
                Title = currTitle != null ? currTitle.Text : "",
                Description = currDesc != null ? currDesc.Text : ""
            };

            return JsonView(true, "_EditMediaPartial", model,
                JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Validates and updates a media item.
        /// </summary>
        /// <param name="model">New data from view.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(MediaInformation model)
        {
            var media = _db.Media.Where(x => x.Id == model.MediaId)
                .FirstOrDefault();

            model.FileName = media.FileName;

            if (null == media)
            {
                ModelState.AddModelError("originalNotFound", Localization.ErrItemNotFoundInDb);
            }
            else
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var language = _db.Language
                            .Where(x => x.LCID == model.LCID).FirstOrDefault();

                        if (!string.IsNullOrEmpty(model.Title))
                        {
                            var title = media.Title
                                .Where(x => x.Language == language).FirstOrDefault();

                            if (null == title)
                            {
                                title = new MediaTitle
                                {
                                    Language = language
                                };

                                media.Title.Add(title);
                            }

                            title.Text = model.Title;
                        }

                        if (!string.IsNullOrEmpty(model.Description))
                        {

                            var description = media.Description
                                .Where(x => x.Language == language).FirstOrDefault();

                            if (null == description)
                            {
                                description = new MediaDescription
                                {
                                    Language = language
                                };

                                media.Description.Add(description);
                            }

                            description.Text = model.Description;
                        }

                        _db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("unexpected", ex.Message);
                }
            }

            return JsonView(ModelState.IsValid, "_EditMediaPartial", model);
        }
        
        /// <summary>
        /// Lists basic file data.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>JSON formatted array of BasicFileData elements.</returns>
        [HttpPost]
        [AjaxErrorHandler]
        public JsonResult GetFileData()
        {
            var media = _db.Media.Include("Project").ToList();
            var fileDataList = new List<BasicFileData>();

            foreach(var elem in media)
            {
                var inProject = elem.Project != null;
                var fileData =
                    new BasicFileData
                    {
                        Id = elem.Id,
                        FileName = elem.FileName,
                        ProjectId = inProject ? elem.Project.Id : -1,
                        ProjectTitle = inProject ? elem.Project.Title.FirstOrDefault().Text : "" // TODO: ML support
                    };

                fileDataList.Add(fileData);
            }

            return Json(new
            {
                Success = true,
                DataList = fileDataList
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxErrorHandler]
        public JsonResult SetProject(int? mediaId, int? projectId)
        {
            if (null == mediaId)
                throw new ArgumentNullException("mediaId",
                    Localization.ErrArgNull);

            var media = _db.Media.Include("Project").Where(x => x.Id == mediaId).FirstOrDefault();

            if (null == media)
                throw new ArgumentException(
                    mediaId.ToString() + " does not exists in Media.",
                    "mediaId");


            if (null == projectId)
                throw new ArgumentNullException("projectId",
                    Localization.ErrArgNull);

            if (-1 == projectId)
            {
                media.Project = null;
            }
            else
            {
                var project = _db.Project.Where(x => x.Id == projectId).FirstOrDefault();

                if (null == project)
                    return Json(new { Success = false, Message = "" });

                media.Project = project;
            }

            _db.SaveChanges();

            var inProject = media.Project != null;

            return Json(
                new
                {
                    Success = true,
                    ProjectTitle = inProject
                        ? media.Project.Title.FirstOrDefault().Text
                        : Localization.NoProject,
                    InProject = inProject,
                    MediaId = media.Id // unused now
                },
                JsonRequestBehavior.AllowGet); // ML support
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxErrorHandler]
        public JsonResult SetNoProject(int? mediaId)
        {
            if (null == mediaId)
                return Json(new { Success = false, Message = "" }); // TODO: error msgs

            var media = _db.Media.Include("Project").Where(x => x.Id == mediaId).FirstOrDefault();

            if (null == media)
                return Json(new { Success = false, Message = "" });


            media.Project = null;
            _db.SaveChanges();

            return Json(new
                {
                    Success = true,
                    MediaId = media.Id
                },
                JsonRequestBehavior.AllowGet); // ML support
        }

        /// <summary>
        /// Returns count of Media entity.
        /// </summary>
        /// <returns>JSON formatted result</returns>
        [HttpPost]
        [AjaxErrorHandler]
        public JsonResult MediaCount()
        {
            var n = _db.Media.Count();

            return Json(new
                {
                    Success = true,
                    Count = n
                },
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AjaxErrorHandler]
        public JsonResult LocalTexts(int? mediaId, int? lcid)
        {
            if (null == mediaId)
                throw new ArgumentNullException("mediaId",
                    Localization.ErrArgNull);

            if (null == lcid)
                throw new ArgumentNullException("lcid",
                    Localization.ErrArgNull);

            var media = _db.Media.Include("Title").Include("Description")
                .Where(x => x.Id == mediaId).FirstOrDefault();

            if (null == media)
                throw new ArgumentException(
                    mediaId.ToString() + " does not exists in Media.",
                    "mediaId");

            var lang = _db.Language.Where(x => x.LCID == lcid).FirstOrDefault();

            if (null == lang)
                throw new ArgumentException(
                    lcid.ToString() + " does not exists in Language.",
                    "lcid");

            var localTitle = media.Title.Where(x => x.Language == lang)
                .FirstOrDefault();

            var localDescription = media.Description
                .Where(x => x.Language == lang).FirstOrDefault();

            return Json(new
                {
                    Success = true,
                    Title = localTitle != null ? localTitle.Text : "",
                    Description = localDescription != null ? localDescription.Text : ""
                }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete media item from database.
        /// </summary>
        /// <param name="mediaId">Id of the item.</param>
        /// <returns>True if item exists in database.</returns>
        private bool Delete(int mediaId)
        {
            var item = _db.Media.Where(x => x.Id == mediaId).FirstOrDefault();

            if (null == item)
                return false;

            var fullPath = Request.MapPath(string.Concat(Macros.UploadPathImgRel, item.FileName));

            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);

            _db.Media.Remove(item);
            _db.SaveChanges();

            return true;
        }

        private DirectoryInfo ServerDirectory(string p)
        {
            return new DirectoryInfo(Path.Combine(HttpRuntime.AppDomainAppPath, p));
        }

        private string FullFilePath(string directory, string fileName)
        {
            return string.Format("{0}\\{1}", directory, fileName);
        }

        /// <summary>
        /// Saves a file to a specified place.
        /// </summary>
        /// <param name="file">The file to save.</param>
        /// <param name="name">Desired filename.</param>
        /// <param name="directory">Target directory.</param>
        private void SaveFile(HttpPostedFileBase file, string name, DirectoryInfo directory)
        {
            string pathString = directory.ToString();

            if (!Directory.Exists(pathString))
                Directory.CreateDirectory(pathString);

            var path = FullFilePath(pathString, name);
            file.SaveAs(path);
        }

        /// <summary>
        /// Generates a filename that not matches any in the database.
        /// </summary>
        /// <returns></returns>
        private string GetUniqueFileName()
        {
            string fileName = Path.GetRandomFileName().Replace(".", "");

            while (_db.Media.Where(x => x.FileName == fileName).Any())
                fileName = Path.GetRandomFileName().Replace(".", "");

            return fileName;
        }
    }
}