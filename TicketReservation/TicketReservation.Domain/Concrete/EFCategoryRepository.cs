using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Domain.Concrete
{
    public class EFCategoryRepository : ICategoryRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Category> Categories
        {
            get { return context.Categories; } 

        }
        public IEnumerable<SubCategory> SubCategories
        {
            get { return context.SubCategories; }
        }

        public IEnumerable<SelectListItem> CategoriesForDropList
        {
            get
            {
                IEnumerable<SelectListItem> selectListItems = new List<SelectListItem>();
                selectListItems = Categories.Select(x => new SelectListItem
                {
                    Value = x.EventCategoryID.ToString(),
                    Text = x.EventCategoryName
                });
                return DefaultItem.Concat(selectListItems);
            }
        }

        public IEnumerable<SelectListItem> SubCategoryForDropList
        {
            get
            {
                IEnumerable<SelectListItem> selectListItems = new List<SelectListItem>();
                selectListItems = SubCategories.Select(x => new SelectListItem
                {
                    Value = x.EventSubcategoryID.ToString(),
                    Text = x.EventSubCategoryName
                });
                return DefaultItem.Concat(selectListItems);
            }
        }

        public IEnumerable<SelectListItem> DefaultItem
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = "",
                    Text = "- SELECT -"
                }, count: 1);
            }
        }
    }
}
