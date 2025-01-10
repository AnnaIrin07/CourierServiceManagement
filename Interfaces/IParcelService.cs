using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CourierManagement
{
interface IParcelService
    {
        void BookParcel(string username);
        void TrackParcel();
        void ViewAllParcels();
    }
}