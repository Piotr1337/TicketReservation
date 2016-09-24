using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Domain.Concrete
{
    public class EFEventRepository : IEventRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Events> Events
        {
            get
            { 
                return context.Events;
            }
        }

        public Events GetEvent(int? eventId)
        {
            Events foundEvent = context.Events.Find(eventId);
            return foundEvent;           
        }

        public void SaveEvent(Events theEvent)
        {
            if (theEvent.EventID == 0)
            {
                context.Events.Add(theEvent);
            }
            else
            {
                Events dbEntry = context.Events.Find(theEvent.EventID);
                if (dbEntry != null)
                {
                    dbEntry.EventName = theEvent.EventName;
                    dbEntry.OtherDetails = theEvent.OtherDetails;
                    dbEntry.EventStartDateTime = theEvent.EventStartDateTime;
                    dbEntry.EventEndDateTime = theEvent.EventEndDateTime;
                    dbEntry.EventCategoryID = theEvent.EventCategoryID;
                    dbEntry.EventSubCategoryID = theEvent.EventSubCategoryID;
                    if (!string.IsNullOrEmpty(theEvent.ImageMimeType))
                    {
                        dbEntry.ImageData = theEvent.ImageData;
                        dbEntry.ImageMimeType = theEvent.ImageMimeType;
                    }
                }
            }
            context.SaveChanges();
        }

        public Events DeleteEvent(int eventId)
        {
            Events dbEntry = context.Events.Find(eventId);
            List<Ticket> dbListTicket = context.Ticket.Where(x => x.EventID == eventId).ToList();
            if (dbEntry != null)
            {
                context.Events.Remove(dbEntry);
                context.Ticket.RemoveRange(dbListTicket);
                context.SaveChanges();
            }
            return dbEntry;           
        }
    }
}
