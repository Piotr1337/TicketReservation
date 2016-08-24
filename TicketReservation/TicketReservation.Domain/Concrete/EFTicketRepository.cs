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
    }
}
