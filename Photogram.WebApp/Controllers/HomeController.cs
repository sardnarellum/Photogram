using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resources;
using Photogram.WebApp.Models;

namespace Photogram.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var setup = _db.Setup.FirstOrDefault();
            ViewBag.MainTitle = setup != null
                ? setup.CurrentMainTitleText()
                : Localization.PhotogramNet;
            ViewBag.Footer = setup != null ? setup.CurrentFooterText() : "";
            ViewBag.Title = Localization.Portfolio;

            return View(_db.Project.Include("ProjectInclude")
                .OrderBy(x => x.Position)
                .Where(x => x.Visible && x.ProjectInclude.Count() > 0)
                .ToArray());
        }

        [ChildActionOnly]
        public ActionResult About()
        {
            var setup = _db.Setup.FirstOrDefault();

            if (null == setup)
            {
                return View(new AboutModel());
            }


            var model = new AboutModel
            {
                Lead = setup.CurrentAboutLeadText(),
                Body = setup.CurrentAboutBodyText(),
                Background = setup.AboutBackground
            };

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult Contact()
        {
            var setup = _db.Setup.FirstOrDefault();

            if (null == setup)
            {
                return View(new ContactModel());
            }

            var model = new ContactModel
            {
                Lead = setup.CurrentContactLeadText(),
                Email = setup.Email,
                Phone = setup.Phone,
                FacebookURL = setup.FacebookURL,
                GitHubURL = setup.GitHubURL,
                InstagramURL = setup.InstagramURL,
                LinkedInURL = setup.LinkedInURL,
                Background = setup.ContactBackground
            };

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult SetLanguage(int? lcid, string returnUrl)
        {
            if (null == lcid)
            {
                return RedirectToAction("Index");
            }

            var lang = _db.Language.Where(x => x.LCID == lcid)
                .FirstOrDefault();

            if (null != lang)
            {
                LanguageManagement.SetLanguage(lang);
                HttpCookie langCookie = new HttpCookie("culture", lang.LCID.ToString());
                langCookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Response.Cookies.Add(langCookie);
            }

            if (!string.IsNullOrEmpty(returnUrl)) {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Redirects()
        {
            return View();
        }

        
        public ActionResult WorksDropDown()
        {
            return PartialView("_WorksDropDownPartial");
        }
    }
}