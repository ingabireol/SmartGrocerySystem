using System.Runtime.CompilerServices;

namespace SmartGroceryMgtSystem.Pages.Models
{
    public class Purchase
    {
        private string? purchaseId;
        private int userId;
        private int groceryId;
        private DateTime purchaseDate;


        public Purchase()
        {
        }
        public string? PurchaseId { get => purchaseId; set => purchaseId = value; }
        public int UserId { get => userId; set => userId = value; }
        public int GroceryId { get => groceryId; set => groceryId = value; }
        public DateTime PurchaseDate { get => purchaseDate; set => purchaseDate = value; }
    }
    
}
