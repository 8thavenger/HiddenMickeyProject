using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HiddenMickeyProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "start-hunt",
            url: "Hunt/{RegionName}",
            defaults: new { controller = "Home", action = "ScavengerHunt" }
        );


            routes.MapRoute(
            name: "scavenger-hunt",
            url: "Hunt/{RegionName}/{AreaName}/{LocationName}",
            defaults: new { controller = "Home", action = "ScavengerHunt",LocationName=UrlParameter.Optional}
        );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}