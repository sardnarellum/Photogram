using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Photogram.WebApp.Models
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString LabelIsActive(this HtmlHelper helper, bool cond)
        {
            return MvcHtmlString.Create(cond ? "active" : "");
        }

        public static MvcHtmlString RawActionLink(this HtmlHelper htmlHelper, string rawHtml, string action, string controller, object routeValues, object htmlAttributes)
        {
            string holder = Guid.NewGuid().ToString();
            string anchor = htmlHelper.ActionLink(holder, action, controller, routeValues, htmlAttributes).ToString();
            return MvcHtmlString.Create(anchor.Replace(holder, rawHtml));
        }
    }
}