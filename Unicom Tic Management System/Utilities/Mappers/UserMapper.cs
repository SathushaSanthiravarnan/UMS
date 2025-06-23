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
    internal static class UserMapper
    {
        public static UserDto ToDTO(User user)
        {
            if (user == null) return null;
            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Nic = user.Nic,
                Role = (UserRole)user.Role, 
                CreatedAt = user.CreatedAt
            };
        }

        public static User ToEntity(UserDto userDto)
        {
            if (userDto == null) return null;
            return new User
            {
                UserId = userDto.UserId,
                Username = userDto.Username,
                Nic = userDto.Nic,
                Role = (UserRole)userDto.Role, 
                CreatedAt = userDto.CreatedAt
            };
        }

        public static List<UserDto> ToDTOList(IEnumerable<User> users)
        {
            return users?.Select(u => ToDTO(u)).ToList() ?? new List<UserDto>();
        }

        public static List<User> ToEntityList(IEnumerable<UserDto> userDtos)
        {
            return userDtos?.Select(u => ToEntity(u)).ToList() ?? new List<User>();
        }
    }
}
