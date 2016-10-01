using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Models
{
    public class IndexAdminViewModel
    {
        public IEnumerable<Events> IndexEvents { get; set; }
        public IEnumerable<Artists> IndexArtists { get; set; }
    }
}