using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Furniture
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Product", "{type}/{meta}",

                new { controller = "Product", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type","san-pham" }
                },
                namespaces:new[] { "Furniture.Controllers" });


            routes.MapRoute("Detail", "{type}/{meta}/{id}",

               new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
               new RouteValueDictionary
               {
                    {"type","san-pham" }
               },
              namespaces: new[] { "Furniture.Controllers" });
           
            routes.MapRoute("AboutUs", "{type}",

               new { controller = "Default", action = "AboutUs" },
               new RouteValueDictionary
               {
                    {"type","about-us" }
               },
              namespaces: new[] { "Furniture.Controllers" });

            routes.MapRoute("News", "{type}",

              new { controller = "News", action = "Index" },
              new RouteValueDictionary
              {
                    {"type","tin-tuc" }
              },
             namespaces: new[] { "Furniture.Controllers" });

            routes.MapRoute("getDetailNews", "{type}/{meta}/{id}",

               new { controller = "News", action = "Detail", meta = UrlParameter.Optional, id = UrlParameter.Optional },
               new RouteValueDictionary
               {
                    {"type","tin-tuc" }
               },
               namespaces: new[] { "Furniture.Controllers" });


            routes.MapRoute("getContact", "{type}",

               new { controller = "Default", action = "getContact" },
               new RouteValueDictionary
               {
                    {"type","contact" }
               },
              namespaces: new[] { "Furniture.Controllers" });

            routes.MapRoute("Cart", "{type}",

               new { controller = "ShoppingCart", action = "Index" },
               new RouteValueDictionary
               {
                    {"type","shoppingcart" }
               },
              namespaces: new[] { "Furniture.Controllers" });

            routes.MapRoute("Payment", "{type}",

               new { controller = "ShoppingCart", action = "CheckOut" },
               new RouteValueDictionary
               {
                    {"type","payment" }
               },
              namespaces: new[] { "Furniture.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] {"Furniture.Controllers"}
            );
        }
    }
}
