using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string? Borrower { get; set; }
        public string? Lender { get; set; }
        public string? ItemName { get; set; }
    }
}
