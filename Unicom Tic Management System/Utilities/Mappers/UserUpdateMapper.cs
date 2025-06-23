using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.UserDtos;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal static class UserUpdateMapper
    {
        
        public static void ToEntity(UserUpdateDto updateDto, User user)
        {
            if (updateDto == null || user == null) return;
            user.Username = updateDto.Username;          
        }

    }

}
