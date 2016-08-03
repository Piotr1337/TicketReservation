using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;
using TicketReservation.Models;

namespace TicketReservation.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IEventRepository repository;
        private ICategoryRepository catRepo;

        public AdminController(IEventRepository repo, ICategoryRepository catRepository)
        {
            repository = repo;
            catRepo = catRepository;
        }

        // GET: Admin
        public ViewResult Index()
        {
            return View(repository.Events);
        }

        public ViewResult Edit(int eventId)
        {
            var theEvent = repository.Events.FirstOrDefault(x => x.EventID == eventId);
            var viewModel = Mapper.Map<Event, AdminViewModel>(theEvent);
            viewModel.Categories = catRepo.Categories;
            viewModel.SubCategories = catRepo.SubCategories;
            viewModel.CategoriesForDropList = catRepo.CategoriesForDropList;           
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Event theEvent)
        {
            var viewModel = Mapper.Map<Event, AdminViewModel>(theEvent);
            if (ModelState.IsValid)
            {
                repository.SaveEvent(theEvent);
                TempData["message"] = string.Format("Zapisano {0}", theEvent.EventName);
                return RedirectToAction("Index");
            }
            else
            {
                return View(viewModel);
            }
        }

        //public ViewResult Create()
        //{
        //    return View("Edit", new AdminViewModel()
        //    {
        //        GetEvent = new Event()
        //        {
        //            EventStartDateTime = DateTime.Now,
        //            EventEndDateTime = DateTime.Now,
        //            TicketsOnSaleDateTime = DateTime.Now
        //        },
        //        Categories = catRepo.Categories,
        //        SubCategories = catRepo.SubCategories
        //    });
        //}

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