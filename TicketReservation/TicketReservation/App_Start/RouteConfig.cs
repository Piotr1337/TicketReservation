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

            routes.MapRoute(null,
                "",
                new
                {
                   controller = "Event", action = "List",
                   category = (string)null
                }
            );

            routes.MapRoute(null, 
                "{category}", 
                new
                {
                    controller = "Event", action = "List"
                }
            );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
