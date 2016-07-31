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
        public IEnumerable<Event> Events
        {
            get
            { 
                return context.Events;
            }
        }

        public void SaveEvent(Event theEvent)
        {
            if (theEvent.EventID == 0)
            {
                context.Events.Add(theEvent);
            }
            else
            {
                Event dbEntry = context.Events.Find(theEvent.EventID);
                if (dbEntry != null)
                {
                    dbEntry.EventName = theEvent.EventName;
                    dbEntry.OtherDetails = theEvent.OtherDetails;
                    dbEntry.EventStartDateTime = theEvent.EventStartDateTime;
                    dbEntry.EventEndDateTime = theEvent.EventEndDateTime;
                    dbEntry.Category = theEvent.Category;
                }
            }
            context.SaveChanges();
        }

        public Event DeleteEvent(int eventId)
        {
            Event dbEntry = context.Events.Find(eventId);
            if (dbEntry != null)
            {
                context.Events.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;           
        }
    }
}
