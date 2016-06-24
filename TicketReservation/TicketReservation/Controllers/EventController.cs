using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketReservation.Domain.Abstract;

namespace TicketReservation.Controllers
{
    public class EventController : Controller
    {
        private IEventRepository repository;

        public EventController(IEventRepository eventRepository)
        {
            this.repository = eventRepository;
        }

        public ViewResult List()
        {
            return View(repository.Events);
        }
    }
}