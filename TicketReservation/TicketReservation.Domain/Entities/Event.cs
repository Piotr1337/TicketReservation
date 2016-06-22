﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservation.Domain.Entities
{
    public class Event
    {
        public int EventID { get; set; }
        public int EventCategoryCode{ get; set; }
        public int ArtistID { get; set; }
        public int PlaceID { get; set; }
        public string EventName { get; set; }
        public DateTime EventStartDateTime  { get; set; }
        public DateTime EventEndDateTime { get; set; }
        public DateTime TicketsOnSaleDateTime { get; set; }
        public string OtherDetails { get; set; }


    }
}