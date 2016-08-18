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
            viewModel.CategoriesForDropList = catRepo.CategoriesForDropList;
            viewModel.SubCategoryForDropList = catRepo.SubCategoryForDropList;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Event theEvent, HttpPostedFileBase image = null)
        {
            var viewModel = Mapper.Map<Event, AdminViewModel>(theEvent);
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    theEvent.ImageMimeType = image.ContentType;
                    theEvent.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(theEvent.ImageData, 0, image.ContentLength);
                }
                ViewData["Category"] = viewModel;
                repository.SaveEvent(theEvent);
                TempData["message"] = string.Format("Zapisano {0}", theEvent.EventName);
                return RedirectToAction("Index");
            }
            else
            {
                return View(viewModel);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new AdminViewModel()
            {

                EventStartDateTime = DateTime.Now,
                EventEndDateTime = DateTime.Now,
                TicketsOnSaleDateTime = DateTime.Now,
                CategoriesForDropList = catRepo.CategoriesForDropList,
                SubCategoryForDropList = catRepo.SubCategoryForDropList,
            });
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