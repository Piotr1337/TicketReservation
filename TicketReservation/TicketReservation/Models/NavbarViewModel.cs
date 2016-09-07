using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Models
{
    public class NavbarViewModel
    {
        public IEnumerable<Categories> Categories { get; set; }
        public IEnumerable<SubCategories> SubCategories { get; set; }
        public MemberLoginViewModel LoginModel { get; set; }
    }
}