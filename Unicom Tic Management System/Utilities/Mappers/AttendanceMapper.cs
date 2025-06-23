using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs;
using Unicom_Tic_Management_System.Models.Enums;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal class AttendanceMapper
    {
        public static AttendanceDto ToDTO(Attendance attendance)
        {
            if (attendance == null) return null;
            return new AttendanceDto
            {
                AttendanceId = attendance.AttendanceId,
                StudentId = attendance.StudentId,
                SubjectId = attendance.SubjectId,
                Date = attendance.Date,
                TimeSlotId = attendance.TimeSlotId,
                Status = (AttendanceStatus)attendance.Status
            };
        }

        public static Attendance ToEntity(AttendanceDto attendanceDto)
        {
            if (attendanceDto == null) return null;
            return new Attendance
            {
                AttendanceId = attendanceDto.AttendanceId,
                StudentId = attendanceDto.StudentId,
                SubjectId = attendanceDto.SubjectId,
                Date = attendanceDto.Date,
                TimeSlotId = attendanceDto.TimeSlotId,
                Status = (AttendanceStatus)attendanceDto.Status
            };
        }

        public static List<AttendanceDto> ToDTOList(IEnumerable<Attendance> attendances)
        {
            return attendances?.Select(ToDTO).ToList() ?? new List<AttendanceDto>();
        }

        public static List<Attendance> ToEntityList(IEnumerable<AttendanceDto> attendanceDtos)
        {
            return attendanceDtos?.Select(ToEntity).ToList() ?? new List<Attendance>();
        }
    }
}
