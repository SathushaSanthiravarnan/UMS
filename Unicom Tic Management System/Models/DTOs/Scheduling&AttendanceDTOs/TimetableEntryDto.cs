using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs
{
    internal class TimetableEntryDto
    {
        public int TimetableId { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public int LecturerId { get; set; }
        public int RoomId { get; set; }
        public string DayOfWeek { get; set; }
        public int TimeSlotId { get; set; }
        public string AcademicYear { get; set; }
        public int GroupId { get; set; }
        public string ActivityType { get; set; }

    }
}
