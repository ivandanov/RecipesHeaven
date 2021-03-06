﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RecipesHeaven.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Category",
                url: "Category/{id}",
                defaults: new { controller = "Category", action = "Category", id = UrlParameter.Optional },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: "Recipe",
                url: "Recipe/{id}",
                defaults: new { controller = "Recipe", action = "Recipe", id = UrlParameter.Optional },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
