using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CourierManagement
{
    // Logger
    static class Logger
    {
        public static void Log(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(Config.LogFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now}] {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logging failed: {ex.Message}");
            }
        }
    }
}