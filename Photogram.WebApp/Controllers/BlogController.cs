using Photogram.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Photogram.WebApp.Controllers
{
    public abstract class BlogBaseController : BaseController
    {
        /// <summary>
        /// Returns a blog post for the corresponding Id if it is correct.
        /// </summary>
        /// <param name="postId">Id of the blogpost</param>
        /// <returns></returns>
        protected ActionResult LoadDetails(int? postId)
        {
            if (null == postId)
            {
                return new HttpStatusCodeResult(503);
            }

            var post = _db.BlogPost.Include("Tags").Where(x => x.Id == postId).FirstOrDefault();

            if (null == post)
            {
                return new HttpNotFoundResult();
            }

            var model = new BlogPostDTO
            {
                Id = post.Id,
                Modified = post.Modified,
                Title = post.Title,
                Lead = post.Lead,
                Body = post.Body,
                Visible = post.Visible,
                Tags = post.Tags
            };

            return View(model);
        }
    }

    [Authorize]
    public class BlogAdminController : BlogBaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var setup = _db.Setup.FirstOrDefault();

            if (null == setup)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError); //TODO: instead of this use initializer
            }

            ViewBag.PageTitle = _db.Setup.FirstOrDefault().BlogName;
            var model = _db.BlogPost.ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var model = new BlogPostDTO();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? postId)
        {
            return LoadDetails(postId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(BlogPostDTO info)
        {
            var modPost = new BlogPost
            {
                Id = info.Id,
                Title = info.Title,
                Lead = info.Lead,
                Body = info.Body,
                Tags = info.Tags,
                Visible = info.Visible,
                Modified = DateTime.Now
            };

            var oldPost = _db.BlogPost.Where(x => x.Id == modPost.Id).FirstOrDefault();

            if (null == oldPost)
            {
                modPost.Id = 0;
                _db.BlogPost.Add(modPost);
            }

            oldPost = modPost;

            var success = true;
            var errorMsg = "";

            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                success = false;
                errorMsg = ex.Message;
            }

            return Json(new { Success = success, Error = errorMsg }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? postId)
        {
            return View();
        }
    }

    public class BlogPublicController : BlogBaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var setup = _db.Setup.FirstOrDefault();

            if (null == setup)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }

            var visible = setup.BlogVisible;

            if (!visible)
                return RedirectToAction("Index", "Home");

            ViewBag.PageTitle = _db.Setup.FirstOrDefault().BlogName;
            var model = _db.BlogPost.Where(x => x.Visible).ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int? postId)
        {
            return LoadDetails(postId);
        }
    }
}