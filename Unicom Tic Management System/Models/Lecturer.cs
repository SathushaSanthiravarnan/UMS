using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.Enums;

namespace Unicom_Tic_Management_System.Models
{
    internal class Lecturer
    {
        public int LecturerId { get; set; } 
        public string Name { get; set; }
        public string Nic { get; set; } 
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int? DepartmentId { get; set; } 
        public DateTime? HireDate { get; set; } 
        public int UserId { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string EmployeeId { get; internal set; }
    }
}
