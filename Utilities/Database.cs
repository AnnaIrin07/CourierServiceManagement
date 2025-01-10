using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.IO;

namespace CourierManagement
{
    // Database Handler
    static class Database
    {
        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Config.ConnectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Database connection failed: " + ex.Message);
            }
        }
    }
}