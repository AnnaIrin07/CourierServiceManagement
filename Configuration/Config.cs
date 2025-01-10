using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CourierManagement
{
    // Centralized Configuration Module
    static class Config
    {
        public static readonly string ConnectionString = "Server=ASDLAPKCH0478\\SQLEXPRESS; Database = ParcelDB; Trusted_Connection = true; TrustServerCertificate=True;";
        public static readonly string LogFilePath = "system.log";
        public static readonly string ReportFilePath = "ParcelReport.csv";
    }
}