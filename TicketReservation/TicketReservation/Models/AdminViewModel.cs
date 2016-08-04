using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using TicketReservation.Domain.Entities;


namespace TicketReservation.Models
{
    public class AdminViewModel
    {
        [Key]
        public int EventID { get; set; }
        [Display(Name = "Kategoria")]
        public int EventCategoryID { get; set; }
        public int ArtistID { get; set; }
        [Display(Name = "Podkategoria")]
        public int EventSubCategoryID { get; set; }
        public int PlaceID { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwe wydarzenia")]
        [Display(Name = "Nazwa")]
        public string EventName { get; set; }

        [DataType(DataType.DateTime), Display(Name = "Data rozpoczęcia")]
        [Required(ErrorMessage = "Proszę podać date rozpoczęcia wydarzenia")]
        public DateTime EventStartDateTime { get; set; }

        [DataType(DataType.DateTime), Display(Name = "Data zakończenia")]
        [Required(ErrorMessage = "Proszę podać date zakończenia wydarzenia")]
        public DateTime EventEndDateTime { get; set; }

        [DataType(DataType.DateTime), Display(Name = "Promocja na bilety")]
        [Required(ErrorMessage = "Proszę podać date promocji")]
        public DateTime TicketsOnSaleDateTime { get; set; }

        [DataType(DataType.MultilineText), Display(Name = "Opis")]
        [Required(ErrorMessage = "Proszę podać opis")]
        public string OtherDetails { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public IEnumerable<SelectListItem> SubCategoryForDropList { get; set; }
        public IEnumerable<SelectListItem> CategoriesForDropList { get; set; }
    }
}