using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Domain.Concrete
{
    public class EFTicketRepository : ITicketRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Ticket> Tickets
        { 
            get { return context.Ticket; }
        }

        public void SaveTicket(Ticket theTicket)
        {
            if (theTicket.TicketID == 0)
            {
                context.Ticket.Add(theTicket);
            }
            context.SaveChanges();
        }

        public Ticket DeleteTicket(int ticketId)
        {
            Ticket dbEntry = context.Ticket.Find(ticketId);
            if (dbEntry != null)
            {
                context.Ticket.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
