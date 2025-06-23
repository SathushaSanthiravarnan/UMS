using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal class TimetableEntryMapper
    {
        public static TimetableEntryDto ToDTO(Timetable timetableEntry)
        {
            if (timetableEntry == null) return null;
            return new TimetableEntryDto
            {
                TimetableId = timetableEntry.TimetableId,
                CourseId = timetableEntry.CourseId,
                SubjectId = timetableEntry.SubjectId,
                LecturerId = timetableEntry.LecturerId,
                RoomId = timetableEntry.RoomId,
                DayOfWeek = timetableEntry.DayOfWeek,
                TimeSlotId = timetableEntry.TimeSlotId,
                AcademicYear = timetableEntry.AcademicYear,              
            };
        }

        public static Timetable ToEntity(TimetableEntryDto timetableEntryDto)
        {
            if (timetableEntryDto == null) return null;
            return new Timetable
            {
                TimetableId = timetableEntryDto.TimetableId,
                CourseId = timetableEntryDto.CourseId,
                SubjectId = timetableEntryDto.SubjectId,
                LecturerId = timetableEntryDto.LecturerId,
                RoomId = timetableEntryDto.RoomId,
                DayOfWeek = timetableEntryDto.DayOfWeek,
                TimeSlotId = timetableEntryDto.TimeSlotId,
                AcademicYear = timetableEntryDto.AcademicYear,
            };
        }

        public static List<TimetableEntryDto> ToDTOList(IEnumerable<Timetable> timetableEntries)
        {
            return timetableEntries?.Select(ToDTO).ToList() ?? new List<TimetableEntryDto>();
        }

        public static List<Timetable> ToEntityList(IEnumerable<TimetableEntryDto> timetableEntryDtos)
        {
            return timetableEntryDtos?.Select(ToEntity).ToList() ?? new List<Timetable>();
        }
    }
}
