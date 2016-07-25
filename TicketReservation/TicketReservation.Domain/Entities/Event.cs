using System;
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
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int EventID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int EventCategoryID{ get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ArtistID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PlaceID { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwe wydarzenia")]
        [Display(Name = "Nazwa")]
        public string EventName { get; set; }

        [DataType(DataType.DateTime), Display(Name = "Data rozpoczęcia")]
        [Required(ErrorMessage = "Proszę podać date rozpoczęcia wydarzenia")]
        public DateTime EventStartDateTime  { get; set; }

        [DataType(DataType.DateTime), Display(Name = "Data zakończenia")]
        [Required(ErrorMessage = "Proszę podać date zakończenia wydarzenia")]
        public DateTime EventEndDateTime { get; set; }

        public DateTime TicketsOnSaleDateTime { get; set; }

        [DataType(DataType.MultilineText), Display(Name="Opis")]
        [Required(ErrorMessage = "Proszę podać opis")]
        public string OtherDetails { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Category> CategoryList { get; set; } 
    }
}
