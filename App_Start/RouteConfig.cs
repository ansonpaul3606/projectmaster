
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PerfectWebERP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Security", action = "login", id = UrlParameter.Optional }
            );
            //        routes.Add("Default", new GuidRoute(
            //"{controller}/{action}/{id}",
            //new
            //{
            //    controller = "Security",
            //    action = "login",
            //    guid = "",
            //    id = UrlParameter.Optional
            //}));

            //        routes.Add("GuidRoute", new GuidRoute(
            //            "g/{guid}/{controller}/{action}/{id}",
            //            new
            //            {
            //                controller = "Security",
            //                action = "login",
            //                guid = "",
            //                id = UrlParameter.Optional
            //            }));
        }
    }
}
