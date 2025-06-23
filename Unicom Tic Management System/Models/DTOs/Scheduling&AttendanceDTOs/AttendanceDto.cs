using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.Enums;

namespace Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs
{
    internal class AttendanceDto
    {
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateTime Date { get; set; }
        public int TimeSlotId { get; set; }
        public AttendanceStatus Status { get; set; } 
    }
}
