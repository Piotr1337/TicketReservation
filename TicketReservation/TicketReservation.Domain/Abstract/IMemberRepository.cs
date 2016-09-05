using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Domain.Abstract
{
    public interface IMemberRepository
    {
        IEnumerable<Members> Members { get; }

        string GetPassword(string email);
    }
}
