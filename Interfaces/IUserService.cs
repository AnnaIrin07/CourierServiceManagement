using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CourierManagement
{
    public interface IUserService
    {
        bool Login(string username, string password, out string message);  // Updated Login method signature
        void RegisterUser(string username, string password);  // Updated RegisterUser to accept parameters
    }


}