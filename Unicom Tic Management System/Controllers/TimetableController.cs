using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs;
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Repositories.Interfaces;
using Unicom_Tic_Management_System.Services;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Controllers
{
    internal class TimetableController
    {
        private readonly ITimetableRepository _timetableRepository;

        public TimetableController()
        {
            _timetableRepository = new TimetableRepository();
        }

        public TimetableController(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository ?? throw new ArgumentNullException(nameof(timetableRepository));
        }

        public string AddTimetableEntry(TimetableEntryDto dto)
        {
            var model = new Timetable
            {
                CourseId = dto.CourseId,
                LecturerId = dto.LecturerId,
                RoomId = dto.RoomId,
                MainGroupId = dto.MainGroupId,
                SubGroupId = dto.SubGroupId,
                SlotId = dto.SlotId,
                Date = dto.Date
            };

            _timetableRepository.AddTimetableEntry(model);
            return "Timetable entry added successfully.";
        }

        public string UpdateTimetableEntry(TimetableEntryDto dto)
        {
            var timetable = new Timetable
            {
                TimetableEntryId = dto.TimetableId,
                CourseId = dto.CourseId,
                LecturerId = dto.LecturerId,
                RoomId = dto.RoomId,
                MainGroupId = dto.MainGroupId,
                SubGroupId = dto.SubGroupId,
                SlotId = dto.SlotId,
                Date = dto.Date
            };

            try
            {
                _timetableRepository.UpdateTimetableEntry(timetable);
                return "Timetable entry updated successfully.";
            }
            catch (ArgumentNullException)
            {
                return "Error: Timetable data cannot be null for update.";
            }
            catch (Exception ex)
            {
                return $"Error updating timetable entry: {ex.Message}";
            }
        }

        public string DeleteTimetableEntry(int timetableId)
        {
            try
            {
                _timetableRepository.DeleteTimetableEntry(timetableId);
                return "Timetable entry deleted successfully.";
            }
            catch (Exception ex)
            {
                return $"Error deleting timetable entry: {ex.Message}";
            }
        }

        public Timetable GetTimetableEntryById(int timetableId)
        {
            try
            {
                return _timetableRepository.GetTimetableEntryById(timetableId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving timetable entry by ID: {ex.Message}");
                return null;
            }
        }

        public List<Timetable> GetAllTimetableEntries()
        {
            try
            {
                return _timetableRepository.GetAllTimetableEntries();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all timetable entries: {ex.Message}");
                return new List<Timetable>();
            }
        }

        public List<Timetable> GetTimetableEntriesByCourse(int courseId, string academicYear = null)
        {
            try
            {
                return _timetableRepository.GetTimetableEntriesByCourse(courseId, academicYear);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving timetable entries by course: {ex.Message}");
                return new List<Timetable>();
            }
        }

        public List<Timetable> GetTimetableEntriesBySubject(int subjectId, string academicYear = null)
        {
            try
            {
                return _timetableRepository.GetTimetableEntriesBySubject(subjectId, academicYear);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving timetable entries by subject: {ex.Message}");
                return new List<Timetable>();
            }
        }

        public List<Timetable> GetTimetableEntriesByLecturer(int lecturerId, string academicYear = null)
        {
            try
            {
                return _timetableRepository.GetTimetableEntriesByLecturer(lecturerId, academicYear);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving timetable entries by lecturer: {ex.Message}");
                return new List<Timetable>();
            }
        }

        public List<Timetable> GetTimetableEntriesByRoom(int roomId, string academicYear = null)
        {
            try
            {
                return _timetableRepository.GetTimetableEntriesByRoom(roomId, academicYear);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving timetable entries by room: {ex.Message}");
                return new List<Timetable>();
            }
        }

        public List<Timetable> GetTimetableEntriesByDayAndTimeSlot(string dayOfWeek, int timeSlotId, string academicYear = null)
        {
            try
            {
                return _timetableRepository.GetTimetableEntriesByDayAndTimeSlot(dayOfWeek, timeSlotId, academicYear);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Input error for day of week: {ex.Message}");
                return new List<Timetable>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving timetable entries by day and time slot: {ex.Message}");
                return new List<Timetable>();
            }
        }

        public List<Timetable> GetTimetableEntriesByAcademicYear(string academicYear)
        {
            try
            {
                return _timetableRepository.GetTimetableEntriesByAcademicYear(academicYear);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving timetable entries by academic year: {ex.Message}");
                return new List<Timetable>();
            }
        }
    }
}

