using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TicketReservation
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
               "Default",                                              // Route name
               "{controller}/{action}/{categoryId}",                           // URL with parameters
               new { controller = "Event", action = "List", categoryId = 1 }  // Parameter defaults
           );


            //routes.MapRoute(null,
            //    "",
            //    new
            //    {
            //       controller = "Event", action = "List",
            //       categoryId = 2
            //    }
            //);

            //routes.MapRoute(null, 
            //    "{category}", 
            //    new
            //    {
            //        controller = "Event", action = "List"
            //    }
            //);

            //routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
