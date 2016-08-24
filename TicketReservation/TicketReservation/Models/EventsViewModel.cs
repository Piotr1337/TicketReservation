using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Models
{
    public class EventsViewModel
    {
        public IEnumerable<Events> Events { get; set; }  
        public Categories CurrentCategory { get; set; }
        public Events Event { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; } 
    }
}