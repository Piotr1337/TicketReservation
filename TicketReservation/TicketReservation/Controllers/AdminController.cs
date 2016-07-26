using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            
            AdminViewModel adm = new AdminViewModel();
            adm.GetEvent = repository.Events.FirstOrDefault(x => x.EventID == eventId);
            adm.Categories = catRepo.Categories;
            adm.SubCategories = catRepo.SubCategories;

            return View(adm);
        }

        [HttpPost]
        public ActionResult Edit(AdminViewModel adminViewModel)
        {
            if (ModelState.IsValid)
            {
                repository.SaveEvent(adminViewModel.GetEvent);
                TempData["message"] = string.Format("Zapisano {0}", adminViewModel.GetEvent.EventName);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
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