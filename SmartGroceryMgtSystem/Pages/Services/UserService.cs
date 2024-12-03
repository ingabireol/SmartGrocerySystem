using Microsoft.Data.SqlClient;
using SmartGroceryMgtSystem.Pages.Models;
using System.Data;

namespace SmartGroceryMgtSystem.Pages.Services
{
    public class UserService
    {
        public UserService() { }
        string conString = "Data Source=DESKTOP-65VK734\\SQLEXPRESS;Initial Catalog=groceries_management_system_db;Integrated Security=True;TrustServerCertificate=True";
        public User LoginUser(string email, string password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    // Query to fetch user by email
                    string sqlQuery = "SELECT * FROM [dbo].[user] WHERE email= @Email";
                    System.Diagnostics.Debug.WriteLine(email+password+" Here");
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Retrieve user details
                                var retrievedPassword = reader["password"]?.ToString();

                                // Check if the password matches
                                if (retrievedPassword == password)
                                {
                                    // Create a User object with retrieved data
                                    User user = new User
                                    (
                                        Convert.ToInt32(reader["user_id"]),
                                        reader["password"].ToString(),
                                        reader["email"].ToString(),
                                        reader["role"].ToString(),
                                        reader["full_name"].ToString()
                                        // Add other properties if needed
                                    );
                                    return user;
                                }
                            }
                        }
                    }
                }

                // Return null if user not found or password mismatch
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public bool SaveUser(User user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    // Insert query
                    string sqlQuery = @"
                INSERT INTO [dbo].[user] (password, email, role, full_name)
                VALUES (@Password, @Email, @Role, @Name)";

                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        // Add parameters to the query
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Role", user.Role);
                        cmd.Parameters.AddWithValue("@Name", user.Name);

                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }
                }

                // Return success message
                return true;
            }
            catch (Exception ex)
            {
                 //.WriteLine("SOme thing went Wrong");
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.StackTrace}");
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public User? GetUserById(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    // Query to fetch user by ID
                    string sqlQuery = "SELECT user_id, password, email, role, full_name FROM Users WHERE UserId = @UserId";

                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        // Add parameter for the ID
                        cmd.Parameters.AddWithValue("@UserId", id);

                        // Execute the query
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate User object with data from the database
                                return new User(
                                    reader.GetInt32(0), // UserId
                                    reader.IsDBNull(1) ? null : reader.GetString(1), // Password
                                    reader.IsDBNull(2) ? null : reader.GetString(2), // Email
                                    reader.IsDBNull(3) ? null : reader.GetString(3), // Role
                                    reader.IsDBNull(4) ? null : reader.GetString(4)  // Name
                                );
                            }
                        }
                    }
                }
                // Return null if no user is found
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }




    }
}
