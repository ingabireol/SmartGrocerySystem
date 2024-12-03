using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SmartGroceryMgtSystem.Pages.Models;

namespace SmartGroceryMgtSystem.Pages.Admin.Product
{
    public class EditModel : PageModel
    {
        public Grocery groceryInfo = new();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string? ID = Request.Query["id"];

            try
            {
                string conString = "Data Source=DESKTOP-65VK734\\SQLEXPRESS;Initial Catalog=groceries_management_system_db;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM [dbo].[grocery] WHERE grocery_id=@ID";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                groceryInfo.Grocery_id =  reader.GetInt32(0);
                                groceryInfo.Name = reader.GetString(1);
                                groceryInfo.Category= reader.GetString(2);
                                groceryInfo.ImageUrl= reader.GetString(5);
                                //groceryInfo.Available = reader.GetString(4);                                
                                //float pr= (float)reader.GetSqlDouble(3);
                                groceryInfo.Price = (float)(double)reader["price"];

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
            string ID = Request.Query["id"];

            groceryInfo.Grocery_id = Int32.Parse(ID);
            groceryInfo.Name = Request.Form["name"];
            groceryInfo.Category = Request.Form["category"];
            //groceryInfo.Available = Request.Form["available"];
            groceryInfo.ImageUrl = Request.Form["image_url"];
            groceryInfo.Price = Convert.ToSingle(Request.Form["price"]);


            if (groceryInfo.Name.Length == 0 || groceryInfo.Category.Length == 0 ||
                Request.Form["price"].ToString().Length == 0 )
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
                    string sqlQuery = "UPDATE [dbo].[grocery] SET name=@name,category=@category, price=@price WHERE grocery_id=@ID"; // ,available = @available
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {

                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@name", groceryInfo.Name);
                        cmd.Parameters.AddWithValue("@category", groceryInfo.Category);
                        cmd.Parameters.AddWithValue("@price", groceryInfo.Price);
                        //cmd.Parameters.AddWithValue("@available", groceryInfo.Available);
                        //cmd.Parameters.AddWithValue("@image_url", groceryInfo.Image_url);

                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }

            groceryInfo.Name = ""; groceryInfo.Price = 0;
            groceryInfo.Category = ""; groceryInfo.Available = "";

            successMessage = "Driver updated successfully!";

            Response.Redirect("/Admin/Driver/Manage");
        }
    }
}
