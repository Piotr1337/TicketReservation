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
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private IMemberRepository _memberRepository;

        public AuthController(IMemberRepository memberRepo)
        {
            _memberRepository = memberRepo;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult MemberLoginSummary()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MemberLoginSummary(AuthModelView model)
        {
            if (!ModelState.IsValid)
            {
                return View("Account", model);
            }

            if (model.LoginModel.Email != null && model.LoginModel.Password == _memberRepository.GetPassword(model.LoginModel.Email))
            {
                var member = _memberRepository.GetMember(model.LoginModel.Email);

                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, member.Email),
                    new Claim(ClaimTypes.Name, member.Email),
                },"ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return RedirectToAction("List","Event");
            }
            ModelState.AddModelError("LoginError","Nieprawidłowy email albo hasło");

            return View("Account", model);
        }

        public ActionResult Account()
        {
            return View();
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("List", "Event");
        }

        [HttpPost]
        public ActionResult MemberRegisterSummary(AuthModelView model)
        {
            if (ModelState.IsValid)
            {
                var toModel = Mapper.Map<MemberRegisterViewModel, Members>(model.RegisterModel);
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