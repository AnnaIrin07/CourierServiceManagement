using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CourierManagement
{
    // Utility Services
    static class Utils
    {
        public static double CalculateCost(double weight, double distance)
        {
            return weight * 5 + distance * 2;
        }

        public static void HandleException(Exception ex)
        {
            Logger.Log("Error: " + ex.Message);
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

}
