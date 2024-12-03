
using Microsoft.Data.SqlClient;
using SmartGroceryMgtSystem.Pages.Models;

namespace SmartGroceryMgtSystem.Pages.Services
{
    public class GroceryService
    {
        private readonly string connectionString = "Data Source=DESKTOP-65VK734\\SQLEXPRESS;Initial Catalog=groceries_management_system_db;Integrated Security=True;TrustServerCertificate=True";

        // Save a new Grocery
        public bool Save(Grocery grocery)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "INSERT INTO grocery (name, category, price, image_url) VALUES (@Name, @Category, @Price, @ImageUrl)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", grocery.Name);
                        cmd.Parameters.AddWithValue("@Category", grocery.Category);
                        cmd.Parameters.AddWithValue("@Price", grocery.Price);
                        cmd.Parameters.AddWithValue("@ImageUrl", grocery.ImageUrl);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }

        // Edit an existing Grocery
        public bool Edit(Grocery grocery)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "UPDATE grocery SET name = @Name, category = @category, price = @Price, image_url = @ImageUrl WHERE grocery_id = @Grocery_id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Grocery_id", grocery.Grocery_id);
                        cmd.Parameters.AddWithValue("@Name", grocery.Name);
                        cmd.Parameters.AddWithValue("@Category", grocery.Category);
                        cmd.Parameters.AddWithValue("@Price", grocery.Price);
                        cmd.Parameters.AddWithValue("@ImageUrl", grocery.ImageUrl);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Delete a Grocery by ID
        public bool Delete(int groceryId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "DELETE FROM grocery WHERE grocery_id = @Grocery_id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Grocery_id", groceryId);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Get a Grocery by ID
        public Grocery GetById(int groceryId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT grocery_id, name, category, price, image_url FROM grocery WHERE grocery_id = @Grocery_id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Grocery_id", groceryId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Grocery
                                {
                                    Grocery_id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Category = reader.GetString(2),
                                    Price = reader.GetFloat(3),
                                    ImageUrl = reader.GetString(4)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        // Get all Groceries
        public List<Grocery> GetAll()
        {
            var groceries = new List<Grocery>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT grocery_id, name, category, price, image_url FROM grocery";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                groceries.Add(new Grocery
                                {
                                    Grocery_id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Category = reader.GetString(2),
                                    Price = Convert.ToSingle(reader[3]),
                                    ImageUrl = reader.GetString(4)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return groceries;
        }
    }
}
