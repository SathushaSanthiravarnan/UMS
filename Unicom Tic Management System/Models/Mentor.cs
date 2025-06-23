using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models
{
    internal class Mentor
    {
        public int MentorId { get; set; } 
        public string Nic { get; set; } 
        public int? DepartmentId { get; set; } 
        public int? UserId { get; set; } 
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Name { get; internal set; }
    }
}
