using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models.DTOs.UserDtos
{
    internal class UserUpdateDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; internal set; }
        public string Nic { get; internal set; }
    }
}
