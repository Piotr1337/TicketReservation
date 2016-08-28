using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Domain.Abstract
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> Tickets { get; }

        void SaveTicket(Ticket theTicket);

        Ticket DeleteTicket(int ticketId);
    }
}
