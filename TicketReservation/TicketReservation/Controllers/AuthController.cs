﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;
using TicketReservation.Models;

namespace TicketReservation.Controllers
{
    public class AuthController : Controller
    {
        private IMemberRepository _memberRepository;

        public AuthController(IMemberRepository memberRepo)
        {
            _memberRepository = memberRepo;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(MemberLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Email != null && model.Password == _memberRepository.GetPassword(model.Email))
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "Xtian"),
                    new Claim(ClaimTypes.Email, "xtiian@gmail.com"),
                    new Claim(ClaimTypes.Country, "Philipines"),   
                },"ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return RedirectToAction("Index","Admin");
            }
            ModelState.AddModelError("","Nieprawidłowy email albo hasło");
            return View(model);
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(MemberLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var toModel = Mapper.Map<MemberLoginViewModel, Members>(model);
                _memberRepository.AddMember(toModel);
            }
            else
            {
                ModelState.AddModelError("","Cos nie tak");
            }
            return View();
        }
    }
}