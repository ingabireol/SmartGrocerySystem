using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace SmartGroceryMgtSystem.Pages.Admin.Driver
{
    public class EditModel : PageModel
    {
        public DriverInfo driverInfo = new DriverInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string ID = Request.Query["user_id"];

            try
            {
                string conString = "Data Source=DESKTOP-65VK734\\SQLEXPRESS;Initial Catalog=groceries_management_system_db;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM [dbo].[user] WHERE user_id=@ID";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                driverInfo.user_id = "" + reader.GetInt32(0);
                                driverInfo.email = reader.GetString(1);
                                driverInfo.password = reader.GetString(2);
                                driverInfo.full_name = reader.GetString(3);
                                driverInfo.role = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + ID);
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            string ID = Request.Query["user_id"];

            driverInfo.user_id = ID;
            driverInfo.email = Request.Form["email"];
            driverInfo.password = Request.Form["password"];
            driverInfo.full_name = Request.Form["full_name"];
           


            if (driverInfo.email.Length == 0 || driverInfo.password.Length == 0 ||
                driverInfo.full_name.Length == 0 )
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                string conString = "Data Source=DESKTOP-65VK734\\SQLEXPRESS;Initial Catalog=groceries_management_system_db;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string sqlQuery = "UPDATE [dbo].[user] SET email=@email,password=@password, full_name=@full_name WHERE user_id=@ID";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {

                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@email", driverInfo.email);
                        cmd.Parameters.AddWithValue("@password", driverInfo.password);
                        cmd.Parameters.AddWithValue("@full_name", driverInfo.full_name);

                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }

            driverInfo.email = ""; driverInfo.password = "";
            driverInfo.full_name = ""; driverInfo.role = "";

            successMessage = "Driver updated successfully!";

            Response.Redirect("/Admin/Driver/Manage");
        }
    }
}
