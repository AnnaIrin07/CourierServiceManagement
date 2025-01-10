using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.IO;

namespace CourierManagement
{
class ReportService : IReportService
    {
        public void GenerateReport()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(Config.ReportFilePath))
                {
                    writer.WriteLine("ParcelId,Sender,Receiver,Cost,Status");

                    using (SqlConnection conn = Database.GetConnection())
                    {
                        string query = "SELECT ParcelId, Sender, Receiver, Cost, Status FROM Parcels";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            writer.WriteLine($"{reader["ParcelId"]},{reader["Sender"]},{reader["Receiver"]},{reader["Cost"]},{reader["Status"]}");
                        }
                    }
                }

                Console.WriteLine($"Report generated: {Config.ReportFilePath}\n");
                Logger.Log("Report generated.");
            }
            catch (Exception GenerateReportException)
            {
                Utils.HandleException(GenerateReportException);
            }
        }
    }
}