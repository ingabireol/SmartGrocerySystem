using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartGroceryMgtSystem.Pages.Models;
using SmartGroceryMgtSystem.Pages.Services;
using static System.Net.Mime.MediaTypeNames;
using System.Web.Helpers;

namespace SmartGroceryMgtSystem.Pages.Admin.Product
{
    public class CreateModel : PageModel
    {
        public GroceryService service = new();
        public string message = "";

        public void OnGet()
        {
            // Initialization logic for GET request (if needed)
        }
        public void OnPost()
        {
            System.Diagnostics.Debug.WriteLine("Inside Post Gorcery");
            Grocery grocery = new Grocery      
            {
                Name = Request.Form["Name"],
                Category = Request.Form["Category"],
                Price = float.Parse(Request.Form["Price"]),
                ImageUrl = "/ds/sd"
            };
                //ImageUrl = Request.Form["ImageUrl"]
                if (service.Save(grocery))
            {
                message = "Saved succesfully";
                return;
            }
            else
            {
                message = "Save Failed";
            }

        }
    }
}


//public void OnPost()
//{
//    Grocery grocery = new Grocery
//    {
//        Name = Request.Form["Name"],
//        Category = Request.Form["Category"],
//        Price = float.Parse(Request.Form["Price"]),
//        ImageUrl = Request.Form["ImageUrl"]
//    };

//    System.Diagnostics.Debug.WriteLine("I am in PostMethod");
//    bool isSaved = service.Save(grocery);

//    if (!isSaved)
//    {
//        message = "Failed to add grocery. Please try again.";
//        return;
//    }

//    Response.Redirect("/Admin/ManageGroceries");
//}
//    }
