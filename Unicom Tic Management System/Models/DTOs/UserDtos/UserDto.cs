using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.Enums;

namespace Unicom_Tic_Management_System.Models.DTOs.UserDtos
{
    internal class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Nic { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
