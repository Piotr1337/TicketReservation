using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Domain.Concrete
{
    public class EFMemberRepository : IMemberRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Members> Members
        {
            get { return context.Members; }
        }

        public string GetPassword(string email)
        {
            Members member = context.Members.FirstOrDefault(x => x.Email == email);
            return member.Password;
        }
    }
}
