using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using TicketReservation.App;
using TicketReservation.App_Start;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;
using TicketReservation.Models;

namespace TicketReservation.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private IMemberRepository _memberRepository;
        private readonly UserManager<AppUser> userManager;

        public AuthController(IMemberRepository memberRepo)
        {
            _memberRepository = memberRepo;
        }

        public AuthController()
       : this(Startup.UserManagerFactory.Invoke())
        {
        }

        public AuthController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        //[HttpGet]
        //public ActionResult Login(string returnUrl)
        //{
        //    var model = new LoginViewModel
        //    {
        //        ReturnUrl = returnUrl
        //    };

        //    return View(model);
        //}

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
                    new Claim(ClaimTypes.Name, member.Email),
                    new Claim(ClaimTypes.Country, "Polska"),
                },"ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }
            ModelState.AddModelError("LoginError","Nieprawidłowy email albo hasło");

            return View("Account", model);
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("List", "Event");
            }

            return returnUrl;
        }

        [HttpGet]
        public ActionResult Account(string returnUrl)
        {
            var model = new AuthModelView()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && userManager != null)
            {
                userManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}