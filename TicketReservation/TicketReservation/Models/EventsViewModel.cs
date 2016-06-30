using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Models
{
    public class EventsViewModel
    {
        public IEnumerable<Event> Events { get; set; }  
        public Category CurrentCategory { get; set; }
    }
}