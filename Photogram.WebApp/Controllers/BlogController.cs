using Photogram.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Photogram.WebApp.Controllers
{
    public class BlogController : BaseController
    {
        // GET: Blog
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(int? postId)
        {
            return LoadDetails(postId);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var model = new BlogPostInformation();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Add(BlogPostInformation info)
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int? postId)
        {
            return LoadDetails(postId);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogPostInformation info)
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? postId)
        {
            return View();
        }

        /// <summary>
        /// Returns a blog post for the corresponding Id if it is correct.
        /// </summary>
        /// <param name="postId">Id of the blogpost</param>
        /// <returns></returns>
        private ActionResult LoadDetails(int? postId)
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

            var model = new BlogPostInformation
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
}