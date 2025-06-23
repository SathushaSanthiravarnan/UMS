using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models.DTOs.StaffDTOs
{
    internal class LecturerDto
    {
        public int LecturerId { get; set; }
        public string Name { get; set; }
        public string Nic { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? HireDate { get; set; }
    }
}
