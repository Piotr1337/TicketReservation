using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Controllers
{

    public class AdminController : Controller
    {
        private IEventRepository repository;

        public AdminController(IEventRepository repo)
        {
            repository = repo;
        }

        // GET: Admin
        public ViewResult Index()
        {
            return View(repository.Events);
        }

        public ViewResult Edit(int eventId)
        {
            Event editEvent = repository.Events
                .FirstOrDefault(e => e.EventID == eventId);

            return View(editEvent);
        }
    }
}