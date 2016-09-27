using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;
using TicketReservation.Infrastructure;
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

        //EVENTY
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
        public ActionResult Delete(int eventId)
        {
            Events deletedEvent = repository.DeleteEvent(eventId);
            if (deletedEvent != null)
            {
                TempData["ticketMessage"] = string.Format("Usunięto {0}", deletedEvent.EventName);
            }
            return RedirectToAction("Index");
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

        public ViewResult Create()
        {
            return View("Edit", new AdminViewModel()
            {
                EventStartDateTime = DateTime.Now,
                EventEndDateTime = DateTime.Now,
                CategoriesForDropList = catRepo.CategoriesForDropList,
                SubCategoryForDropList = new[] { new SelectListItem { Value = "", Text = "" } },
                Events = new Events()
            });
        }

        //BILETY
        public ViewResult AddTicket(string date, int eventId)
        {
            return View("TicketEdit", new TicketViewModel()
            {
                DateOfEvent = DateTime.Parse(date),
                EventID = eventId
            });
        }

        public ActionResult DeleteTicket(int ticketId)
        {
            Ticket deletedTicket = ticketRepository.DeleteTicket(ticketId);
            if (deletedTicket != null)
            {
                TempData["ticketMessage"] = string.Format("Usunięto {0}", deletedTicket.Title);
                return Redirect(Request.UrlReferrer.PathAndQuery);
            }
            return View("Index");
        }

        public ViewResult TicketEdit(int ticketId)
        {
            var theTicket = ticketRepository.Tickets.FirstOrDefault(x => x.TicketID == ticketId);
            var viewModel = Mapper.Map<Ticket, TicketViewModel>(theTicket);

            return View(viewModel);
        }

        public IEnumerable<Ticket> LoadAllTickets()
        {
            var result = ticketRepository.Tickets;
            List<Ticket> ticketList = new List<Ticket>();
            foreach (var item in result)
            {
                Ticket ticket = new Ticket();
                ticket.EventID = item.EventID;
                ticket.TicketID = item.TicketID;
                ticket.DateOfEvent = item.DateOfEvent;
                ticket.Title = item.Title;
                ticket.Location = item.Location;
                ticket.Price = item.Price;

                ticketList.Add(ticket);
            }

            return ticketList;
        }

        public JsonResult GetTickets(int eventID)
        {
            var myTickets = LoadAllTickets();

            var ticketList = from e in myTickets
                             select new
                             {
                                 id = e.TicketID,
                                 price = e.Price,
                                 title = e.Title,
                                 date = e.DateOfEvent,
                                 location = e.Location,
                                 eventID = e.EventID,
                                 beginDate = repository.GetEvent(eventID).EventStartDateTime.Value.ToShortDateString(),
                                 finishDate = repository.GetEvent(eventID).EventEndDateTime.Value.ToShortDateString()
                             };
            var rows = ticketList.ToArray();
            return Json(rows.Where(x => x.eventID == eventID), JsonRequestBehavior.AllowGet);
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

        //ARTYSCI
        public ViewResult AddArtist()
        {
            return View("ArtistiEdit", new ArtistViewModel()
            {
                CategoriesForDropList = catRepo.CategoriesForDropList,
            });
        }

        //public ViewResult EditArtist(int artistId)
        //{
        //    return View();
        //}

        //KATEGORIE
        public ActionResult GetSubcategory(int id)
        {
            return Json(PopulateSubCategory(id), JsonRequestBehavior.AllowGet);
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