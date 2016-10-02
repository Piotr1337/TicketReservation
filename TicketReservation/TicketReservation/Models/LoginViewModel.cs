using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketReservation.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Proszę podać nazwę użytkownika")]
        [Display(Name = "użytkownik")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Proszę podać hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "hasło")]
        public string Password { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}