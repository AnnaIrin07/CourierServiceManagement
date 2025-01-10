using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CourierManagement
{
class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message) { }
    }
}