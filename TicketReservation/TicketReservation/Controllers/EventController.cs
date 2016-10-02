using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;
using TicketReservation.Models;

namespace TicketReservation.Controllers
{
    [AllowAnonymous]
    public class EventController : Controller
    {
        private IEventRepository repository;
        private ICategoryRepository categoryRep;
          

        public EventController(IEventRepository eventRepository, ICategoryRepository categoryRepository)
        {
            this.repository = eventRepository;
            this.categoryRep = categoryRepository;
        }

        [ChildActionOnly]
        public ActionResult NavBar()
        {
            NavbarViewModel model = new NavbarViewModel
            {
                Categories = categoryRep.Categories,
                SubCategories = categoryRep.SubCategories
            };

            return PartialView("NavBarSummary", model);
        }

        public ViewResult List(int? categoryId,int? subcategoryId)
        {
            if (subcategoryId.HasValue)
            {
                EventsViewModel model = new EventsViewModel
                {
                    Events = repository.Events.Where(x => x.EventSubCategoryID == subcategoryId && x.EventSubCategoryID == subcategoryId)

                };
                return View(model);
            }
            else
            {
                EventsViewModel model = new EventsViewModel
                {
                    Events = repository.Events.Where(x => x.EventCategoryID == categoryId)
                };
                return View(model);
            }
        }

        public ViewResult ShowEvent(int? eventId)
        {
            var foundEvent = repository.GetEvent(eventId);
            return View(foundEvent);
        }

        public FileContentResult GetImage(int? eventId)
        {
            var theEvent= repository.GetEvent(eventId);
            if (theEvent != null)
            {
                return File(theEvent.ImageData, theEvent.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}