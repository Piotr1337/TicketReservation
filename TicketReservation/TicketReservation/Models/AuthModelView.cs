﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketReservation.Models
{
    public class AuthModelView
    {
        public MemberLoginViewModel LoginModel { get; set; }
        public MemberRegisterViewModel RegisterModel { get; set; }
    }
}