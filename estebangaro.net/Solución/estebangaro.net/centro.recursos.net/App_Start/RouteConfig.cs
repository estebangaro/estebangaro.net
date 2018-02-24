using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace centro.recursos.net
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AcercaDeRoute",
                url: "AcercaDe/Esteban/{action}/{controller}",
                defaults: new { controller = "Home", action = "QuienSoy" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "GaRoNET", id = UrlParameter.Optional }
            );
        }
    }
}
