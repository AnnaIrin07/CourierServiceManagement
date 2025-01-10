using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CourierManagement
{
class Parcel
    {
        public string ParcelId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public double Weight { get; set; }
        public double Distance { get; set; }
        public double Cost { get; set; }
        public string Status { get; set; }
    }
}