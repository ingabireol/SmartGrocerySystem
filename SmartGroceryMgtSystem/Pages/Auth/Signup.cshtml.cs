using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartGroceryMgtSystem.Pages.Models;
using SmartGroceryMgtSystem.Pages.Services;

namespace SmartGroceryMgtSystem.Pages.Auth
{
    public class SignupModel : PageModel
    {
        UserService userService = new();
        public string message = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            string email = Request.Form["email"];
            string password = Request.Form["password"];
            string name  = Request.Form["fullname"];
            

            if (name.Length == 0 || password.Length == 0 || email.Length == 0)
            {
                message = "All Field Are required";
            }
            else
            {
                User user = new User();
                user.Name = name;
                user.Password = password;
                user.Email = email;
                user.Role = "customer";
                message  = userService.SaveUser(user)?"Saved successfully":"Signup failed";
                //Response.Redirect("/Auth/Signup");

            }






        }
    }
}
