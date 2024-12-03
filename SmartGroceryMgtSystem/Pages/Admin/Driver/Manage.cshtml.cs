using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static SmartGroceryMgtSystem.Pages.Admin.Driver.ManageModel;

namespace SmartGroceryMgtSystem.Pages.Admin.Driver
{
    public class ManageModel : PageModel
    {
        public List<DriverInfo> listDrivers = new List<DriverInfo>();
        public void OnGet()
        {
            listDrivers.Clear();
            try
            {
                string conString = "Data Source=DESKTOP-65VK734\\SQLEXPRESS;Initial Catalog=groceries_management_system_db;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string sqlQuery = "SELECT email, password, full_name, role,user_id  FROM [dbo].[user] where role = 'DRIVER'";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DriverInfo driverInfo = new DriverInfo();
                                driverInfo.user_id = reader.GetInt32(4);
                                driverInfo.email = reader.GetString(0);
                                driverInfo.password = reader.GetString(1);
                                driverInfo.full_name = reader.GetString(2);
                                driverInfo.role = reader.GetString(3);
                                

                                listDrivers.Add(driverInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public class DriverInfo
        {
            public int user_id;
            public string email;
            public string password;
            public string full_name;
            public string role;
        }
    }
}
