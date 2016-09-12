using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketReservation.Models
{
    public class MemberLoginViewModel
    {

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Proszę podać email")]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Proszę podać hasło")]
        public string Password { get; set; }
    }
}