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
        void AddTimetableEntry(TimetableEntryDto timetableDto);
        void UpdateTimetableEntry(TimetableEntryDto timetableDto);
        void DeleteTimetableEntry(int timetableId);
        TimetableEntryDto GetTimetableEntryById(int timetableId);
        List<TimetableEntryDto> GetTimetableEntriesByCourse(int courseId, string academicYear = null);
        List<TimetableEntryDto> GetTimetableEntriesBySubject(int subjectId, string academicYear = null);
        List<TimetableEntryDto> GetTimetableEntriesByLecturer(int lecturerId, string academicYear = null);
        List<TimetableEntryDto> GetTimetableEntriesByRoom(int roomId, string academicYear = null);
        List<TimetableEntryDto> GetTimetableEntriesByDayAndTimeSlot(string dayOfWeek, int timeSlotId, string academicYear = null);
        List<TimetableEntryDto> GetTimetableEntriesByAcademicYear(string academicYear);
        List<TimetableEntryDto> GetAllTimetableEntries();
    }
}

