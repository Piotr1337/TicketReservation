using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TicketReservation.Models
{
    public class ArtistViewModel
    {

        public int ArtistID { get; set; }

        [DisplayName("Imię")]
        public string FirstName { get; set; }

        [DisplayName("Nazwisko")]
        public string LastName { get; set; }

        [DisplayName("Informacje o zespole/artyście")]
        public string Description { get; set; }

        public string ImageData { get; set; }

        public int? EventID { get; set; }

        public string CategoryID { get; set; }

        [DisplayName("Pseudonim artystyczny")]
        public string Nickname { get; set; }

        public bool? IsBand { get; set; }

        [DisplayName("Nazwa zespołu")]
        public string BandName { get; set; }

        public string ImageMimeType { get; set; }
    }
}