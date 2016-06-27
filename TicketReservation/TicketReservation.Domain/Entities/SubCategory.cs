using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservation.Domain.Entities
{
    public class SubCategory
    {
        [Key]
        public int EventSubcategoryID { get; set; }
        public int EventCategoryID { get; set; }
        public string EventSubCategoryName { get; set; }
        public Category Category { get; set; }
    }
}
