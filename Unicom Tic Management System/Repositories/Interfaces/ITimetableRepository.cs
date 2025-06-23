using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface ITimetableRepository
    {
        void AddTimetableEntry(Timetable timetable);
        void UpdateTimetableEntry(Timetable timetable);
        void DeleteTimetableEntry(int timetableId);
        Timetable GetTimetableEntryById(int timetableId);     
        List<Timetable> GetTimetableEntriesByCourse(int courseId, string academicYear = null);
        List<Timetable> GetTimetableEntriesBySubject(int subjectId, string academicYear = null);
        List<Timetable> GetTimetableEntriesByLecturer(int lecturerId, string academicYear = null);
        List<Timetable> GetTimetableEntriesByRoom(int roomId, string academicYear = null);
        List<Timetable> GetTimetableEntriesByDayAndTimeSlot(string dayOfWeek, int timeSlotId, string academicYear = null);
        List<Timetable> GetTimetableEntriesByAcademicYear(string academicYear);
        List<Timetable> GetAllTimetableEntries();

    }
}
