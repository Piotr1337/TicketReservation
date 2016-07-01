using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Domain.Abstract
{
    public interface IEventRepository
    {
        IEnumerable<Event> Events { get; }

        void SaveEvent(Event theEvent);

        Event DeleteEvent(int eventId);


    }
}
