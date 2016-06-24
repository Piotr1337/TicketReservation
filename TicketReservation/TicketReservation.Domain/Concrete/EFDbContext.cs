using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
    }
}
