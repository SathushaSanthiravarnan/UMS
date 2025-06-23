using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.UserDtos;
using Unicom_Tic_Management_System.Models.Enums;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface IUserService
    {
        UserDto GetUserById(int userId);
        UserDto GetUserByUsername(string username);
        UserDto GetUserByNic(string nic);
        List<UserDto> GetAllUsers();
        List<UserDto> GetUsersByRole(UserRole role);
        void RegisterUser(UserRegistrationDto registrationDto);
        void DeleteUser(int userId);
        void UpdateUser(UserDto userDto);

    }
}
