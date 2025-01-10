using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CourierManagement
{
interface IReportService
    {
        void GenerateReport();
    }
}