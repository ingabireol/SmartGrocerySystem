using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartGroceryMgtSystem.Pages.Models;
using SmartGroceryMgtSystem.Pages.Services;

namespace SmartGroceryMgtSystem.Pages.Auth
{
    public class LoginModel : PageModel
    {
        public UserService service = new();
        public string message = "";
        public void OnGet()
        {

        }
        public void OnPost()
        {
            string email = Request.Form["email"].ToString();
            string password = Request.Form["password"].ToString();
            User user = new();
            System.Diagnostics.Debug.WriteLine("I am in PostMethod");
            user = service.LoginUser(email, password);
            if(user == null)
            {
                message = "user not found";
                return;
            }
            if(user.Role.ToUpper() == "ADMIN")
            {
                Response.Redirect("/Admin/Dashboard");
            }
            else if (user.Role.ToUpper() == "ADMIN")
            {
                Response.Redirect("/Customer/Dashboard");
            }
            else if(user.Role.ToUpper() == "Driver")
            {
                Response.Redirect("/Driver/Dashboard");
            }
        }
    }
}
