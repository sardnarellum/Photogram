using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Photogram.WebApp.Models
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString LabelIsActive(this HtmlHelper helper, bool cond)
        {
            return MvcHtmlString.Create(cond ? "active" : "");
        }
    }
}