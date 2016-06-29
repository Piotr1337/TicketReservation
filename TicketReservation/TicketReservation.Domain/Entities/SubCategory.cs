using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("EventCategoryID")]
        public virtual Category Category { get; set; }
    }
}
