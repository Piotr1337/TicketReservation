using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Web;
using System.Web.Mvc;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;
using TicketReservation.Models;

namespace TicketReservation.Controllers
{
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
            CategoryViewModel model = new CategoryViewModel
            {
                Categories = categoryRep.Categories,
                SubCategories = categoryRep.SubCategories
            };
            return PartialView("NavBarSummary", model);
        }

        public ViewResult List()
        {
            EventsViewModel model = new EventsViewModel
            {
                Events = repository.Events
            };
            
            return View(model);
        }
    }
}