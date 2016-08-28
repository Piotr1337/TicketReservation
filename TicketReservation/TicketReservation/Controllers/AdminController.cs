using System;
using System.Collections;
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
        private ITicketRepository ticketRepository;

        public AdminController(IEventRepository repo, ICategoryRepository catRepository, ITicketRepository ticketRepo)
        {
            repository = repo;
            catRepo = catRepository;
            ticketRepository = ticketRepo;
        }

        // GET: Admin
        public ViewResult Index()
        {
            return View(repository.Events);
        }

        public ViewResult Edit(int eventId)
        {
            var theEvent = repository.Events.FirstOrDefault(x => x.EventID == eventId);
            var viewModel = Mapper.Map<Events, AdminViewModel>(theEvent);

            viewModel.CategoriesForDropList = catRepo.CategoriesForDropList;
            viewModel.SubCategoryForDropList = PopulateSubCategory(viewModel.EventCategoryID);
            viewModel.Events = repository.Events.FirstOrDefault(x => x.EventID == eventId);
            ViewBag.IsAdmin = true;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Events theEvent, HttpPostedFileBase image = null)
        {
            var viewModel = Mapper.Map<Events, AdminViewModel>(theEvent);
            if (ModelState.IsValid)
            {
                if (image != null)
                {                 
                    viewModel.ImageMimeType = image.ContentType;
                    viewModel.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(viewModel.ImageData, 0, image.ContentLength);
                }
                ViewData["Category"] = viewModel;
                var toModel = Mapper.Map<AdminViewModel, Events>(viewModel);
                repository.SaveEvent(toModel);
                TempData["message"] = string.Format("Zapisano {0}", theEvent.EventName);
                return RedirectToAction("Index");
            }
            else
            {
                return View(viewModel);
            }
        }

        public ViewResult AddTicket(int eventId)
        {
            return View("TicketEdit", new TicketViewModel()
            {
                DateOfEvent = DateTime.Now,
                EventID = eventId
            });
        }

        [HttpPost]
        public ActionResult DeleteTicket(int ticketId)
        {
            Ticket deletedTicket = ticketRepository.DeleteTicket(ticketId);
            if (deletedTicket != null)
            {
                TempData["ticketMessage"] = string.Format("Usunięto {0}", deletedTicket.Title);
                return View("Index");
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult TicketEdit(Ticket theTicket)
        {
            var viewModel = Mapper.Map<Ticket, TicketViewModel>(theTicket);
            if (ModelState.IsValid)
            {
                var toModel = Mapper.Map<TicketViewModel, Ticket>(viewModel);
                ticketRepository.SaveTicket(toModel);
                TempData["ticketMessage"] = string.Format("Zapisano bilet {0}", toModel.Title);
            }
            return RedirectToAction("Edit","Admin", new { eventId = viewModel.EventID});
        }

        public ViewResult Create()
        {
            return View("Edit", new AdminViewModel()
            {
                EventStartDateTime = DateTime.Now,
                EventEndDateTime = DateTime.Now,
                TicketsOnSaleDateTime = DateTime.Now,
                CategoriesForDropList = catRepo.CategoriesForDropList,
                SubCategoryForDropList = new[] {new SelectListItem {Value = "", Text = ""}}
            });
        }

        public ActionResult GetSubcategory(int id)
        {
            return Json(PopulateSubCategory(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int eventId)
        {
            Events deletedEvent = repository.DeleteEvent(eventId);
            if (deletedEvent != null)
            {
                TempData["ticketMessage"] = string.Format("Usunięto {0}", deletedEvent.EventName);
            }
            return RedirectToAction("Index");
        }

        public IEnumerable<SelectListItem> PopulateSubCategory(int id)
        {
            IEnumerable<SubCategories> sub = new List<SubCategories>();
            IEnumerable<SelectListItem> selectListItems = new List<SelectListItem>();
            sub = catRepo.SubCategories.Where(x => x.EventCategoryID == id);
            selectListItems = sub.Select(x => new SelectListItem
            {
                Value = x.EventSubCategoryID.ToString(),
                Text = x.EventSubCategoryName
            });

            return selectListItems;
        }
    }
}