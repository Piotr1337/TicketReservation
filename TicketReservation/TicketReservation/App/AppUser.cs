using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TicketReservation.App
{
    public class AppUser : IdentityUser
    {
        public string Country { get; set; }

        public int Age { get; set; }
    }
}