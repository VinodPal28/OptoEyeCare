using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OptoEyeCare
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name:"Dashboard",
                url:"Dashboard",
                 defaults: new { controller = "Dashboard", action = "Index" }
            );

            routes.MapRoute(
               name: "contactlense",
               url: "contact-lense",
                defaults: new { controller = "ContactLenses", action = "Index" }
            );

            routes.MapRoute(
                name: "frame",
                url: "frame",
                 defaults: new { controller = "frame", action = "Index" }
            );
            routes.MapRoute(
               name: "lense",
               url: "lense",
                defaults: new { controller = "lenses", action = "Index" }
           );
            routes.MapRoute(
               name: "registration",
               url: "registration",
                defaults: new { controller = "registrationApprove", action = "Index" }
           );
            routes.MapRoute(
              name: "CYL",
              url: "CYL",
               defaults: new { controller = "CYL", action = "Index" }
          );
            routes.MapRoute(
             name: "SPH",
             url: "SPH",
              defaults: new { controller = "SPH", action = "Index" }
         );
            routes.MapRoute(
               name: "axis",
               url: "axis",
            defaults: new { controller = "axis", action = "Index" }
            );
             routes.MapRoute(
             name: "addition",
             url: "addition",
              defaults: new { controller = "addition", action = "Index" }
             );
            routes.MapRoute(
             name: "vision",
             url: "vision",
              defaults: new { controller = "vision", action = "Index" }
             );

            routes.MapRoute(
             name: "Order",
             url: "Order",
              defaults: new { controller = "OrderHistory", action = "orderHistory" }
             );
            routes.MapRoute(
            name: "download",
            url: "download",
             defaults: new { controller = "Download", action = "Index" }
            );
            routes.MapRoute(
            name: "SMS",
            url: "SMS",
             defaults: new { controller = "SMS", action = "Index" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
