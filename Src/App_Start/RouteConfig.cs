using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ex3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Display",
                url: "display/{ip}/{port}",
                defaults: new { controller = "Main", action = "Display" }
            );

            routes.MapRoute(
                name: "DisplayTravel",
                url: "display/{ip}/{port}/{frequency}",
                defaults: new { controller = "Main", action = "DisplayTravel" }
            );

            routes.MapRoute(
                name: "SaveTravel",
                url: "save/{ip}/{port}/{frequency}/{time}/{filename}",
                defaults: new { controller = "Main", action = "SaveTravel" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Main", action = "Default", id = UrlParameter.Optional }
            );
        }
    }
}
