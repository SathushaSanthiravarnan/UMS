using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.Enums;

namespace Unicom_Tic_Management_System.Models
{
    internal class Student
    {
        public int StudentId { get; set; } 
        public string Name { get; set; }
        public string Nic { get; set; } 
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; } 
        public GenderType Gender { get; set; } 
        public DateTime EnrollmentDate { get; set; }
        public int CourseId { get; set; } 
        public int UserId { get; set; } 
        public int MainGroupId { get; set; } 
        public int SubGroupId { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string AdmissionNumber { get; internal set; }
    }
}
