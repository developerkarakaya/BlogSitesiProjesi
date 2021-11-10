using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "article",
                url: "{id}--{Title}",
                defaults: new { controller = "Article", action = "Detail" },
                namespaces:new[] { "MyBlog.Controllers" }
                );
            routes.MapRoute(
               name: "category",
               url: "category/{id}--{categoryName}",
               defaults: new { controller = "category", action = "Index", categoryName = UrlParameter.Optional }
               );

            routes.MapRoute(
                name:"tag",
                url:"tag/{id}--{tagName}",
               defaults: new { controller = "tag", action = "Index", tagName = UrlParameter.Optional }

                );
            routes.MapRoute(
             name: "arcives",
             url: "{yil}-{ay}",
             defaults: new { controller = "arcives", action = "Index", yil = UrlParameter.Optional, ay = UrlParameter.Optional },
              namespaces: new[] { "MyBlog.Controllers" }
             );
            routes.MapRoute(name: "hakkimda", url: "hakkimda", defaults: new { controller = "Home", action = "hakkimda", id = UrlParameter.Optional },
                namespaces: new[] { "MyBlog.Controllers" }
                );
            routes.MapRoute(name: "search", url: "search", defaults: new { controller = "Home", action = "Search", id = UrlParameter.Optional },
               namespaces: new[] { "MyBlog.Controllers" }
               );
            routes.MapRoute(name: "iletisim", url: "iletisim", defaults: new { controller = "Home", action = "iletisim", id = UrlParameter.Optional },
                namespaces: new[] { "MyBlog.Controllers" }
                );
            routes.MapRoute(name: "NotFound", url: "NotFound", defaults: new { controller = "Error", action = "NotFound", id = UrlParameter.Optional },
                namespaces: new[] { "MyBlog.Controllers" } 
                );



            // defaultun her zaman en altta olması gerek.
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MyBlog.Controllers" }
            );


            routes.MapMvcAttributeRoutes();


        }
    }
}
