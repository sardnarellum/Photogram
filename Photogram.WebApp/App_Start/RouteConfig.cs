using System.Web.Mvc;
using System.Web.Routing;

namespace Photogram.WebApp
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "AdminOverride",
            //    url: "Admin/{*.}",
            //    defaults: new { controller = "Dashboard", action = "Index"}
            //);

            routes.MapRoute(
                name: "RenderImage",
                url: "Image/{file}",
                defaults: new { controller = "Image", action = "Render", file = "" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
