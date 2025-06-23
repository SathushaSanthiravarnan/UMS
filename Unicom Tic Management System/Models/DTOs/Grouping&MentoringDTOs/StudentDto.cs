using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models.DTOs.Grouping_MentoringDTOs
{
    internal class StudentDto
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Nic { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Enums.GenderType Gender { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public int MainGroupId { get; set; }
        public int SubGroupId { get; set; }
        public string AdmissionNumber { get; set; }
    }
}
