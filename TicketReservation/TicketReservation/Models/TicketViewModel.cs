﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Models
{
    public class TicketViewModel
    {
        [Key]
        public int TicketID { get; set; }

        public int EventID { get; set; }

        [DisplayName("Artysta/Zespół")]
        public int ArtistID { get; set; }

        [DisplayName("Data wydarzenia")]
        public DateTime DateOfEvent { get; set; }

        [DisplayName("Godzina wydarzenia")]
        public TimeSpan TimeOfEvent { get; set; }

        [DisplayName("Miejscowość")]
        public string Location { get; set; }

        [DisplayName("Cena biletu")]
        public decimal Price { get; set; }

        [DisplayName("Tytuł")]
        public string Title { get; set; }

        public IEnumerable<SelectListItem> ArtistList { get; set; }

        public virtual Events Events { get; set; }
    }
}