using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs
{
    public class TimetableEntryDto
    {
        internal int SubjectId;
        internal string SubjectName;
        

        public int TimetableId { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public int LecturerId { get; set; }
        public string LecturerName { get; set; }

        public int RoomId { get; set; }
        public string RoomNumber { get; set; }

        public int MainGroupId { get; set; }
        public string MainGroupName { get; set; }

        public int SubGroupId { get; set; }
        public string SubGroupName { get; set; }

        public int SlotId { get; set; }
        public string SlotTimeRange { get; set; }

        public DateTime Date { get; set; }
    }

}

