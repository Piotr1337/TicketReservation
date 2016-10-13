using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
            var foundEvent = context.Events.Find(eventId);
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
                    var mapping = Mapper.Map(theEvent, dbEntry);
                    if (!string.IsNullOrEmpty(theEvent.ImageMimeType))
                    {
                        mapping.ImageData = theEvent.ImageData;
                        mapping.ImageMimeType = theEvent.ImageMimeType;
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
