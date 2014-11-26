using Photogram.WebApp.Models;
using System;
using System.Linq;
using System.Web.Mvc;

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
    }
}