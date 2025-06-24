using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.Enums;
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Utilities;

namespace Unicom_Tic_Management_System.Datas
{
    internal class DatabaseSeeder
    {
        private const string ADMIN_USERNAME = "admin@unicom.com";
        private const string ADMIN_PASSWORD = "adminpassword";
        private const string ADMIN_NIC = "123456789V";

        public static void SeedAdminUser() 
        {
            var userRepository = new UserRepository();
            // Call synchronous methods
            var adminUser = userRepository.GetUserByUsername(ADMIN_USERNAME); 

            if (adminUser == null)
            {
                var newAdmin = new User
                {
                    Username = ADMIN_USERNAME,
                    PasswordHash = PasswordHasher.Hash(ADMIN_PASSWORD), 
                    Nic = ADMIN_NIC, 
                    Role = UserRole.Admin, 
                    CreatedAt = DateTime.Now, 
                    UpdatedAt = DateTime.Now 
                };

                try
                {
                    userRepository.AddUser(newAdmin); // No await
                    Console.WriteLine("Default admin user created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to create default admin user: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Default admin user already exists.");
            }
        }
    }
}

