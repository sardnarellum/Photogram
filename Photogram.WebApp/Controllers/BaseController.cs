using Photogram.WebApp.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Resources;
using System.Web;

namespace Photogram.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected PhotogramEntities _db;

        #region Constructor / Dispose

        /// <summary>
        /// Instantiate controller.
        /// </summary>
        public BaseController()
        {
            _db = new PhotogramEntities();

            if (_db.Language.Count() == 0)
            {
                var hun = new Language
                {
                    LCID = 1038
                };

                var eng = new Language
                {
                    LCID = 1033
                };

                _db.Language.Add(hun);
                _db.Language.Add(eng);


                _db.SaveChanges();
            }

            var setup = _db.Setup.FirstOrDefault();

            ViewBag.Years = new SelectList(
                    Enumerable.Range(DateTime.Now.Year - 50, DateTime.Now.Year)
                    .OrderByDescending(year => year)
                    .Select(year => new SelectListItem
                    {
                        Value = year.ToString(),
                        Text = year.ToString()
                    }
                ), "Value", "Text");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        #endregion

        #region Override

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            int lcid = 1033;
            HttpCookie langCookie = Request.Cookies["culture"];

            if (langCookie != null)
            {
                lcid = int.Parse(langCookie.Value);
            }
            else
            {
                var userLanguage = Request.UserLanguages;
                var userLang = userLanguage != null ? userLanguage[0] : "";
                if (userLang != "")
                {
                    LanguageManagement.SetLanguage(userLang);
                    return base.BeginExecuteCore(callback, state);
                }
            }

            LanguageManagement.SetLanguage(lcid);

            return base.BeginExecuteCore(callback, state);
        }

        #endregion

        #region AJAX

        protected JsonResult JsonView(bool success, string viewName
            , object model, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return Json(
                new { Success = success,
                      View = RenderPartialView(viewName, model)
                    }
                , behavior);
        }

        protected JsonResult JsonView(bool success, string viewName1
            , object model1, string viewName2, object model2
            , JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return Json(new
            {
                Success = success,
                View1 = RenderPartialView(viewName1, model1),
                View2 = RenderPartialView(viewName2, model2)
            }, behavior);
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

        #endregion

        #region Common helpers


        #endregion
    }

    /// <summary>
    /// Error handler for JSONresult actions.
    /// </summary>
    public class AjaxErrorHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// Exception handler for JSONresult actions.
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                filterContext.Result = new JsonResult
                {
                    Data = new { Success = false, Error = filterContext.Exception.Message.ToString() },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }    
}