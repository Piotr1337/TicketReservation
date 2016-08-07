﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TicketReservation.Domain.Entities
{
    public class Event
    {
        public int EventID { get; set; }
        public int EventCategoryID{ get; set; }
        public int EventSubCategoryID { get; set; }
        public int ArtistID { get; set; }
        public int PlaceID { get; set; }
        public string EventName { get; set; }
        public DateTime EventStartDateTime  { get; set; }
        public DateTime EventEndDateTime { get; set; }
        public DateTime TicketsOnSaleDateTime { get; set; }
        public string OtherDetails { get; set; }
        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
