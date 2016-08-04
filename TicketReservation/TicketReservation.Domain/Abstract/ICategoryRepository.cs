using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Domain.Abstract
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        IEnumerable<SubCategory> SubCategories { get; }
        IEnumerable<SelectListItem> CategoriesForDropList { get; }
        IEnumerable<SelectListItem> SubCategoryForDropList { get; }

    }
}
