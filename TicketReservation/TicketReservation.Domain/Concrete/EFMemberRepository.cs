using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;
using TicketReservation.Domain.SecurityLibrary;



namespace TicketReservation.Domain.Concrete
{
    public class EFMemberRepository : IMemberRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Members> Members
        {
            get { return context.Members; }
        }

        public void AddMember(Members member)
        {
            var encryptedPassword = CustomEncrypt.Encrypt(member.Password);
            var user = context.Members.Create();
            user.Email = member.Email;
            user.Password = encryptedPassword;
            context.Members.Add(user);
            context.SaveChanges();
        }

        public Members GetMember(string email)
        {
            Members member = context.Members.FirstOrDefault(x => x.Email == email);
            return member;
        }

        public string GetPassword(string email)
        {
            Members mem = new Members();
            mem = context.Members.FirstOrDefault(x => x.Email == email);
            var decryptedPassword = "";
            if (mem != null)
            {
                var materializePassword = mem.Password;
                decryptedPassword = CustomDecrypt.Decrypt(materializePassword);
            }
            else
            {
               decryptedPassword = "";
            }


            return decryptedPassword;

        }
    }
}
