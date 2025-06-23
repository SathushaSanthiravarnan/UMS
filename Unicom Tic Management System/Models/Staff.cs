using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models
{
    internal class Staff
    {
        public int StaffId { get; set; } 
        public string Name { get; set; }
        public string Nic { get; set; } 
        public int? DepartmentId { get; set; } 
        public string EmployeeId { get; internal set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public DateTime? HireDate { get; set; } 
        public int UserId { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
