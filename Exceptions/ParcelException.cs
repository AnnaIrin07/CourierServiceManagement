using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CourierManagement
{
   // Custom Exceptions
    class ParcelException : Exception
    {
        public ParcelException(string message) : base(message) { }
    }

    
}

    