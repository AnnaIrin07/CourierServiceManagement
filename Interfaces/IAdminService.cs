using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CourierManagement
{
interface IAdminService
    {
        List<User> GetAllUsers();
        void ManageParcels();
        void GenerateReports();
    }
} 