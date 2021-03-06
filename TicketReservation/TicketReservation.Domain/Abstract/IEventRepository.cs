﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Domain.Abstract
{
    public interface IEventRepository
    {
        IEnumerable<Events> Events { get; }     

        Events GetEvent(int? eventId);

        void SaveEvent(Events theEvent);

        Events DeleteEvent(int eventId);
    }
}
