using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Models
{
    public class AdminViewModel
    {
        public Event GetEvent { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }
    }
}