using SinanDolayman.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SinanDolayman
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");          

           routes.Add("ArticleDetails", new SeoFriendlyRoute("Article/Details/{id}",
           new RouteValueDictionary(new { controller = "Article", action = "Details" }),
           new MvcRouteHandler()));

            routes.Add("BookDetails", new SeoFriendlyRoute("Book/Details/{id}",
         new RouteValueDictionary(new { controller = "Book", action = "Details" }),
         new MvcRouteHandler()));

            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
          );
        }
    }
}
