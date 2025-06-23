using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.UserDtos;
using Unicom_Tic_Management_System.Models.Enums;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Controllers
{
    internal class UserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // ✅ Register a new user (used in UserForm)
        public void RegisterUser(UserRegistrationDto registrationDto)
        {
            _userService.RegisterUser(registrationDto); 
        }

        public List<UserDto> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        public UserDto GetUserById(int userId)
        {
            return _userService.GetUserById(userId);
        }

        public UserDto GetUserByUsername(string username)
        {
            return _userService.GetUserByUsername(username);
        }

        public UserDto GetUserByNIC(string nic)
        {
            return _userService.GetUserByNic(nic);
        }

        public List<UserDto> GetUsersByRole(UserRole role)
        {
            return _userService.GetUsersByRole(role);
        }

        public void DeleteUser(int userId)
        {
            _userService.DeleteUser(userId);
        }

        public void UpdateUser(UserDto dto)
        {
            _userService.UpdateUser(dto);
        }
    }
}

