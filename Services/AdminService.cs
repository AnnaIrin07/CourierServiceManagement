using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.IO;

namespace CourierManagement
{
class AdminService : IAdminService
    {
        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("GetAllUsers", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                UserId = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Role = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            return users;
        }


        public void ManageParcels()
        {
            Console.WriteLine("Managing parcels...");
            
        }

        public void GenerateReports()
        {
            Console.WriteLine("Generating reports...");
            
        }
    }
} 