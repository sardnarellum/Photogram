using Photogram.WebApp.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using System.Net;

namespace Photogram.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected PhotogramEntities _db;

        #region Constructor

        /// <summary>
        /// Instantiate controller.
        /// </summary>
        public BaseController()
        {
            _db = new PhotogramEntities();

            if (_db.Setup.Count() == 0)
            {
                var hun = new Language
                {
                    Code = "hun",
                    Name = "magyar"
                };

                var eng = new Language
                {
                    Code = "eng",
                    Name = "angol"
                };

                var s = new Setup
                {
                    Email = "info@andrasmuller.com",
                    MainTitle = new TextValue[]
                {
                    new TextValue
                    {
                        Text = "MÜLLER ANDRÁS FOTOGRÁFUS",
                        Language = hun
                    },

                    new TextValue
                    {
                        Text = "ANDRÁS MÜLLER PHOTOGRAPHER",
                        Language = eng
                    },

                }
                };

                _db.Setup.Add(s);


                _db.SaveChanges();
            }

            ViewBag.MainTitle = _db.Setup.First().MainTitle.Where(x => x.Language.Code == "hun")
                .FirstOrDefault().Text; // TODO: ML support

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

        #endregion

        #region AJAX

        protected JsonResult JsonView(bool success, string viewName, object model)
        {
            return Json(new { Success = success, View = RenderPartialView(viewName, model) });
        }

        protected JsonResult JsonView(bool success, string viewName1, object model1, string viewName2, object model2)
        {
            return Json(new
            {
                Success = success,
                View1 = RenderPartialView(viewName1, model1),
                View2 = RenderPartialView(viewName2, model2)
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
                    Data = new { success = false, error = filterContext.Exception.Message.ToString() },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}