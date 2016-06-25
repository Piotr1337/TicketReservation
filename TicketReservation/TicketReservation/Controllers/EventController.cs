using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Web;
using System.Web.Mvc;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;

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
            //IObservable<Event> observable = repository.Events.ToObservable(Scheduler.Default);
            //observable.Subscribe(repository => View(repository.EventName));
            
            return View(repository.Events);
        }
    }
}