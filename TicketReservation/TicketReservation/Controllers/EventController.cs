﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public ViewResult List(int? categoryId)
        {

            EventsViewModel model = new EventsViewModel
            {
                Events = repository.Events
                .Where(x => categoryId == null || x.EventSubCategoryID == categoryId),
                CurrentCategory = categoryRep.Categories.Single(x => x.EventCategoryID == categoryId),
            };

            //EventsViewModel model = new EventsViewModel
            //{
            //    Events = repository.Events.Where(x => x.Category.EventCategoryName == categoryName)
            //};

            return View(model);
        }

        public FileContentResult GetImage(int eventId)
        {
            Event theEvent = repository.Events.FirstOrDefault(e => e.EventID == eventId);
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