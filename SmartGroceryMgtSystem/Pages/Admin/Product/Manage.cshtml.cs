using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartGroceryMgtSystem.Pages.Models;
using SmartGroceryMgtSystem.Pages.Services;

namespace SmartGroceryMgtSystem.Pages.Admin.Product
{
    public class ManageModel : PageModel
    {
        public  GroceryService _groceryService = new();

        public List<Grocery> Groceries { get; set; } = new();

        
        public void OnGet()
        {
            Groceries = _groceryService.GetAll();
        }
    }
}
