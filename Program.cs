// Title : Courier Management System Application
// Author: Anna Irin Anil
// Created at : 22/12/2024
// Updated at : 26/12/2024
// Reviewed by : Sabapathi Shanmugam
// Reviewed at : 30/12/2024
using System;

namespace CourierManagement
{
    class Program
    {
        // Default Admin Credentials
        private static readonly string AdminUsername = "admin";
        private static readonly string AdminPassword = "password123";

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Smart Courier Management System\n");

            IUserService userService = new UserService(Config.ConnectionString);
            IParcelService parcelService = new ParcelService();
            IAdminService adminService = new AdminService();

            while (true)
            {
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. User");
                Console.WriteLine("3. Exit\n");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AdminLogin(adminService, parcelService);
                        break;

                    case "2":
                        UserMenu(userService, parcelService);
                        break;

                    case "3":
                        Console.WriteLine("Thank you for using Smart Courier Management System. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        // Admin Login and Menu
        private static void AdminLogin(IAdminService adminService, IParcelService parcelService)
        {
            Console.Write("\nEnter Admin Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Admin Password: ");
            string password = ReadPassword();

            if (username == AdminUsername && password == AdminPassword)
            {
                Console.WriteLine("\nAdmin login successful!\n");
                AdminMenu(adminService, parcelService);
            }
            else
            {
                Console.WriteLine("\nInvalid Admin credentials. Please try again.\n");
            }
        }

        private static void AdminMenu(IAdminService adminService, IParcelService parcelService)
        {
            while (true)
            {
                Console.WriteLine("Admin Menu:");
                Console.WriteLine("1. View All Users");
                Console.WriteLine("2. View All Parcels");
                Console.WriteLine("3. Generate Reports");
                Console.WriteLine("4. Logout\n");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        adminService.GetAllUsers();
                        break;

                    case "2":
                        parcelService.ViewAllParcels();
                        break;

                    case "3":
                        adminService.GenerateReports();
                        break;

                    case "4":
                        Console.WriteLine("Admin logged out.\n");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        // User Menu with Registration and Login
        private static void UserMenu(IUserService userService, IParcelService parcelService)
        {
            while (true)
            {
                Console.WriteLine("1. User Registration");
                Console.WriteLine("2. User Login");
                Console.WriteLine("3. Exit\n");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("\nEnter a username: ");
                        string newUsername = Console.ReadLine();

                        Console.Write("Enter a password: ");
                        string newPassword = ReadPassword();

                        try
                        {
                            userService.RegisterUser(newUsername, newPassword);
                            Console.WriteLine("Registration successful!\n");
                        }
                        catch (Exception UserMenuException)
                        {
                            Console.WriteLine($"Error during registration: {UserMenuException.Message}\n");
                        }
                        break;

                    case "2":
                        Console.Write("\nEnter your username: ");
                        string username = Console.ReadLine();

                        Console.Write("Enter your password: ");
                        string password = Console.ReadLine();
                        // string password = ReadPassword();

                        if (userService.Login(username, password, out string message))
                        {
                            Console.WriteLine($"\n{message} Welcome, {username}.\n");
                            UserActions(parcelService, username);
                        }
                        else
                        {
                            Console.WriteLine($"\n{message}\n");
                        }
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        // User Actions Menu
        private static void UserActions(IParcelService parcelService, string username)
        {
            while (true)
            {
                Console.WriteLine("User Menu:");
                Console.WriteLine("1. Book Parcel");
                Console.WriteLine("2. Track Parcel");
                Console.WriteLine("3. Generate Reports");
                Console.WriteLine("4. Logout\n");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        parcelService.BookParcel(username);
                        break;

                    case "2":
                        parcelService.TrackParcel();
                        break;

                    case "3":
                        ReportService reportService = new ReportService();
                        reportService.GenerateReport();
                        break;


                    case "4":
                        Console.WriteLine("User logged out.\n");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        // Helper Method for Masking Password Input
        private static string ReadPassword()
        {
            string password = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b");
                    password = password[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    password += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
    }
}
