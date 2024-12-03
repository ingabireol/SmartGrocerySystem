using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace SmartGroceryMgtSystem.Pages.Admin.Driver
{
    public class CreateModel : PageModel
    {

        public DriverInfo driverInfo = new DriverInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            driverInfo.email = Request.Form["email"];
            driverInfo.password = Request.Form["password"];
            driverInfo.full_name = Request.Form["full_name"];
            driverInfo.role = "DRIVER";

            if (driverInfo.email.Length == 0 || driverInfo.password.Length == 0
                || driverInfo.full_name.Length == 0 || driverInfo.role.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }
            //save the data
            try
            {
                string conString = "Data Source=DESKTOP-65VK734\\SQLEXPRESS;Initial Catalog=groceries_management_system_db;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string sqlQuery = "INSERT INTO  [dbo].[user] (email,password,full_name,role) VALUES (@email, @password, @full_name, @role)";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {

                        cmd.Parameters.AddWithValue("@email", driverInfo.email);
                        cmd.Parameters.AddWithValue("@password", driverInfo.password);
                        cmd.Parameters.AddWithValue("@full_name", driverInfo.full_name);
                        cmd.Parameters.AddWithValue("@role", driverInfo.role);

                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                errorMessage = ex.Message;
                return;
            }
            driverInfo.email = ""; driverInfo.password = "";
            driverInfo.full_name = ""; driverInfo.role = "";
            successMessage = "New driver added successfully!";
            //Response.Redirect("Admin/Driver/Manage");

        }
    }

	public class DriverInfo
	{
		public string user_id;
		public string email;
		public string password;
		public string full_name;
		public string role;

	}
}
