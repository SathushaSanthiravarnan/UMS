using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.UserDtos;
using Unicom_Tic_Management_System.Models.Enums;
using Unicom_Tic_Management_System.Repositories.Interfaces;
using Unicom_Tic_Management_System.Services.Interfaces;
using Unicom_Tic_Management_System.Utilities;
using Unicom_Tic_Management_System.Utilities.Mappers;

namespace Unicom_Tic_Management_System.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly INicDetailsRepository _nicRepository;

        public UserService(IUserRepository userRepository, INicDetailsRepository nicRepository)
        {
            _userRepository = userRepository;
            _nicRepository = nicRepository;
        }

        public void RegisterUser(UserRegistrationDto registrationDto)
        {
            if (string.IsNullOrWhiteSpace(registrationDto.Username))
                throw new ArgumentException("Username is required");

            if (string.IsNullOrWhiteSpace(registrationDto.PasswordHash))
                throw new ArgumentException("Password is required");

            var nicDetail = _nicRepository.GetNICDetailByNIC(registrationDto.Nic);
            if (nicDetail == null)
                throw new Exception("NIC not found in authorized list");

            if (nicDetail.IsUsed)
                throw new Exception("This NIC is already used");

            string hashedPassword = PasswordHasher.Hash(registrationDto.PasswordHash);
            registrationDto.PasswordHash = hashedPassword;

            var user = UserRegistrationMapper.ToEntity(registrationDto);
            _userRepository.AddUser(user);

            nicDetail.IsUsed = true;
            _nicRepository.UpdateNICDetail(nicDetail);
        }

        public void DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
        }

        public void UpdateUser(UserDto userDto)
        {
            var existing = _userRepository.GetUserById(userDto.UserId);
            if (existing == null)
                throw new Exception("User not found.");

            var updatedUser = UserMapper.ToEntity(userDto);
            _userRepository.UpdateUser(updatedUser);
        }

        public UserDto GetUserById(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            return UserMapper.ToDTO(user);
        }

        public UserDto GetUserByUsername(string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            return UserMapper.ToDTO(user);
        }

        public UserDto GetUserByNic(string nic)
        {
            var user = _userRepository.GetUserByNIC(nic);
            return UserMapper.ToDTO(user);
        }

        public List<UserDto> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return UserMapper.ToDTOList(users);
        }

        public List<UserDto> GetUsersByRole(UserRole role)
        {
            var all = _userRepository.GetAllUsers();
            var filtered = all.FindAll(u => u.Role == role);
            return UserMapper.ToDTOList(filtered);
        }
    }
}

