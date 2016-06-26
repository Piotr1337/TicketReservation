using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReservation.Domain.Entities
{
    public class Category
    {
        [Key]
        public int EventCategoryID { get; set; }
        public string EventCategoryDescription { get; set; }
        public string EventCategoryName { get; set; }
        public List<SubCategory> SubCategories = new List<SubCategory>();

        public List<SubCategory> SubCategories1
        {
            get { return SubCategories; }
        }
    }
}
