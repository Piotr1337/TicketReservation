using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Concrete;

namespace TicketReservation.Domain.Entities
{
    public class Category
    {
        public Category()
        {
            SubCategories = new List<SubCategory>();
        }
        [Key]
        public int EventCategoryID { get; set; }
        public string EventCategoryDescription { get; set; }
        public string EventCategoryName { get; set; }
        public string Icon { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
