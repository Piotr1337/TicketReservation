using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketReservation.Controllers
{
    public class AuthController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnrl)
        {
            //var model = new LoginModel
            return View();
        }
    }
}