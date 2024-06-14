using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseTracker.Models.ViewModel
{
    public class ExpenseVM
    {
        public Expense expense { get; set; }
        public IEnumerable<SelectListItem> TypeDropDown= Enumerable.Empty<SelectListItem>();
    }
}