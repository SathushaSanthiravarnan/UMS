using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface ITimetableEntryService
    {
        TimetableEntryDto GetTimetableEntryById(int timetableId);
        List<TimetableEntryDto> GetAllTimetableEntries();
        List<TimetableEntryDto> GetTimetableForCourse(int courseId, string academicYear);
        List<TimetableEntryDto> GetTimetableForLecturer(int lecturerId, string academicYear);
        List<TimetableEntryDto> GetTimetableForRoom(int roomId, string academicYear);
        List<TimetableEntryDto> GetTimetableForDay(string dayOfWeek, string academicYear);
        void AddTimetableEntry(TimetableEntryDto entryDto);
        void UpdateTimetableEntry(TimetableEntryDto entryDto);
        void DeleteTimetableEntry(int timetableId);
    }
}

