using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models.DTOs.UserDtos
{
    internal class UserLoginDto
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
