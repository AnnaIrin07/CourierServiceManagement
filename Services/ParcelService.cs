using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace CourierManagement
{
    class ParcelService : IParcelService
    {
        public void BookParcel(string username)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    // Validate that the user exists using a stored procedure
                    using (SqlCommand checkUserCmd = new SqlCommand("CheckUserExists", conn))
                    {
                        checkUserCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        checkUserCmd.Parameters.AddWithValue("@Username", username);

                        int userExists = (int)checkUserCmd.ExecuteScalar();

                        if (userExists == 0)
                        {
                            Console.WriteLine($"User '{username}' does not exist. Please register first.\n");
                            return;
                        }
                    }

                    // Proceed with booking the parcel
                    Console.Write("Enter Sender Name: ");
                    string sender = Console.ReadLine();

                    Console.Write("Enter Receiver Name: ");
                    string receiver = Console.ReadLine();

                    Console.Write("Enter Parcel Weight (kg): ");
                    double weight = double.Parse(Console.ReadLine());

                    Console.Write("Enter Distance (km): ");
                    double distance = double.Parse(Console.ReadLine());

                    Console.Write("Enter Place (Delivery Location): ");
                    string place = Console.ReadLine();

                    double cost = Utils.CalculateCost(weight, distance);

                    // Call stored procedure to insert a parcel
                    using (SqlCommand cmd = new SqlCommand("BookParcel", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Sender", sender);
                        cmd.Parameters.AddWithValue("@Receiver", receiver);
                        cmd.Parameters.AddWithValue("@Weight", weight);
                        cmd.Parameters.AddWithValue("@Distance", distance);
                        cmd.Parameters.AddWithValue("@Cost", cost);
                        cmd.Parameters.AddWithValue("@Status", "booked");
                        cmd.Parameters.AddWithValue("@BookedBy", username);
                        cmd.Parameters.AddWithValue("@Place", place);

                        cmd.ExecuteNonQuery();
                    }

                    Console.WriteLine($"Parcel booked successfully! Cost: ${cost:F2}\n");
                    Logger.Log($"Parcel booked: Sender: {sender}, Receiver: {receiver}, Cost: {cost:F2}, Booked By: {username}, Place: {place}");
                }
            }
            catch (Exception BookParcelException)
            {
                Utils.HandleException(BookParcelException);
            }
        }

        public void TrackParcel()
        {
            try
            {
                Console.Write("Enter Parcel ID: ");
                string parcelId = Console.ReadLine();

                using (SqlConnection conn = Database.GetConnection())
                {
                    // Call stored procedure to get parcel details
                    using (SqlCommand cmd = new SqlCommand("GetParcelStatusAndLocation", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ParcelId", parcelId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine($"Current Status: {reader["Status"]}");
                                Console.WriteLine($"Current Location: {reader["Place"]}\n");
                            }
                            else
                            {
                                Console.WriteLine("Parcel not found.\n");
                            }
                        }
                    }
                }
            }
            catch (Exception TrackParcelException)
            {
                Utils.HandleException(TrackParcelException);
            }
        }

        public void ViewAllParcels()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    // Call stored procedure to get all parcels
                    using (SqlCommand cmd = new SqlCommand("GetAllParcels", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Console.WriteLine("\nParcels:\n");
                            Console.WriteLine($"{"Parcel ID",-15}{"BookedBy",-15}{"Sender",-15}{"Receiver",-15}{"Weight",-10}{"Distance",-10}{"Cost",-10}{"Status",-15}{"Place",-20}");
                            Console.WriteLine(new string('-', 130));

                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["ParcelId"],-15}{reader["BookedBy"],-15}{reader["Sender"],-15}{reader["Receiver"],-15}{reader["Weight"],-10}{reader["Distance"],-10}{reader["Cost"],-10}{reader["Status"],-15}{reader["Place"],-20}");
                            }

                            Console.WriteLine();
                        }
                    }
                }
            }
            catch (Exception ViewAllParcelsException)
            {
                Utils.HandleException(ViewAllParcelsException);
            }
        }
    }
}
