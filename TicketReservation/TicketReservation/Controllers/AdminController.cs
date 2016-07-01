using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Controllers
{
    [Authorize]
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

        [HttpPost]
        public ActionResult Edit(Event theEvent)
        {
            if (ModelState.IsValid)
            {
                repository.SaveEvent(theEvent);
                TempData["message"] = string.Format("Zapisano {0}", theEvent.EventName);
                return RedirectToAction("Index");
            }
            else
            {
                return View(theEvent);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Event());
        }

        [HttpPost]
        public ActionResult Delete(int eventId)
        {
            Event deletedEvent = repository.DeleteEvent(eventId);
            if (deletedEvent != null)
            {
                TempData["message"] = string.Format("Usunięto {0}", deletedEvent.EventName);
            }
            return RedirectToAction("Index");
        }
    }
}