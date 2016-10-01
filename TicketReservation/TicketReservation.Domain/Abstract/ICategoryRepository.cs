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
        IEnumerable<Categories> Categories { get; }
        IEnumerable<SubCategories> SubCategories { get; }
        IEnumerable<SelectListItem> CategoriesForDropList { get; }
        string GetCategory(int categoryId);

    }
}
