using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.UserDtos;
using Unicom_Tic_Management_System.Models.Enums;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal static class UserRegistrationMapper
    {
        public static User ToEntity(UserRegistrationDto registrationDto)
        {
            if (registrationDto == null) return null;
            return new User
            {
                Username = registrationDto.Username,
                PasswordHash = registrationDto.PasswordHash, 
                Nic = registrationDto.Nic,
                Role = (UserRole)registrationDto.Role,
                CreatedAt = DateTime.UtcNow 
            };
        }
    }

}
