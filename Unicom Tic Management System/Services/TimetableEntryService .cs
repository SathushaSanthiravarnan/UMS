using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs;
using Unicom_Tic_Management_System.Repositories.Interfaces;
using Unicom_Tic_Management_System.Services.Interfaces;
using Unicom_Tic_Management_System.Utilities.Mappers;

namespace Unicom_Tic_Management_System.Services
{
    internal class TimetableService : ITimetableService
    {
        private readonly ITimetableRepository _timetableRepository;

        // Dependency Injection for the repository
        public TimetableService(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }

        public void AddTimetableEntry(TimetableEntryDto timetableDto)
        {
            if (timetableDto == null)
            {
                throw new ArgumentNullException(nameof(timetableDto), "Timetable entry data cannot be null.");
            }
            // --- Business Logic / Validation Examples ---
            if (timetableDto.CourseId <= 0)
            {
                throw new ArgumentException("Course must be selected.", nameof(timetableDto.CourseId));
            }
            if (timetableDto.LecturerId <= 0)
            {
                throw new ArgumentException("Lecturer must be selected.", nameof(timetableDto.LecturerId));
            }
            if (timetableDto.RoomId <= 0)
            {
                throw new ArgumentException("Room must be selected.", nameof(timetableDto.RoomId));
            }
            if (timetableDto.SlotId <= 0)
            {
                throw new ArgumentException("Time Slot must be selected.", nameof(timetableDto.SlotId));
            }
            if (timetableDto.Date == default(DateTime))
            {
                throw new ArgumentException("Date must be provided.", nameof(timetableDto.Date));
            }

            // You might add more complex validation here, e.g.:
            // - Check for lecturer availability at that time/date
            // - Check for room availability
            // - Check for group conflicts

            // Convert DTO to Model using your Mapper
            var timetable = TimetableEntryMapper.ToEntity(timetableDto);

            _timetableRepository.AddTimetableEntry(timetable);
        }

        public void UpdateTimetableEntry(TimetableEntryDto timetableDto)
        {
            if (timetableDto == null)
            {
                throw new ArgumentNullException(nameof(timetableDto), "Timetable entry data cannot be null.");
            }
            if (timetableDto.TimetableId <= 0)
            {
                throw new ArgumentException("Invalid Timetable ID for update.", nameof(timetableDto.TimetableId));
            }
            // Add similar business logic/validation as in AddTimetableEntry

            // Convert DTO to Model using your Mapper
            var timetable = TimetableEntryMapper.ToEntity(timetableDto);

            _timetableRepository.UpdateTimetableEntry(timetable);
        }

        public void DeleteTimetableEntry(int timetableId)
        {
            if (timetableId <= 0)
            {
                throw new ArgumentException("Invalid Timetable ID for deletion.", nameof(timetableId));
            }
            // Add business logic, e.g., check if the entry has associated attendance records
            _timetableRepository.DeleteTimetableEntry(timetableId);
        }

        public TimetableEntryDto GetTimetableEntryById(int timetableId)
        {
            if (timetableId <= 0)
            {
                throw new ArgumentException("Invalid Timetable ID.", nameof(timetableId));
            }
            var timetable = _timetableRepository.GetTimetableEntryById(timetableId);
            // Convert Model to DTO using your Mapper
            return TimetableEntryMapper.ToDTO(timetable);
        }

        public List<TimetableEntryDto> GetTimetableEntriesByCourse(int courseId, string academicYear = null)
        {
            if (courseId <= 0)
            {
                throw new ArgumentException("Invalid Course ID.", nameof(courseId));
            }
            var timetables = _timetableRepository.GetTimetableEntriesByCourse(courseId, academicYear);
            return timetables.ConvertAll(TimetableEntryMapper.ToDTO); // Convert list using mapper
        }

        public List<TimetableEntryDto> GetTimetableEntriesBySubject(int subjectId, string academicYear = null)
        {
            // Assuming SubjectId maps to CourseId based on previous context
            return GetTimetableEntriesByCourse(subjectId, academicYear);
        }

        public List<TimetableEntryDto> GetTimetableEntriesByLecturer(int lecturerId, string academicYear = null)
        {
            if (lecturerId <= 0)
            {
                throw new ArgumentException("Invalid Lecturer ID.", nameof(lecturerId));
            }
            var timetables = _timetableRepository.GetTimetableEntriesByLecturer(lecturerId, academicYear);
            return timetables.ConvertAll(TimetableEntryMapper.ToDTO);
        }

        public List<TimetableEntryDto> GetTimetableEntriesByRoom(int roomId, string academicYear = null)
        {
            if (roomId <= 0)
            {
                throw new ArgumentException("Invalid Room ID.", nameof(roomId));
            }
            var timetables = _timetableRepository.GetTimetableEntriesByRoom(roomId, academicYear);
            return timetables.ConvertAll(TimetableEntryMapper.ToDTO);
        }

        public List<TimetableEntryDto> GetTimetableEntriesByDayAndTimeSlot(string dayOfWeek, int timeSlotId, string academicYear = null)
        {
            if (string.IsNullOrWhiteSpace(dayOfWeek))
            {
                throw new ArgumentException("Day of week cannot be empty.", nameof(dayOfWeek));
            }
            if (timeSlotId <= 0)
            {
                throw new ArgumentException("Invalid Time Slot ID.", nameof(timeSlotId));
            }
            var timetables = _timetableRepository.GetTimetableEntriesByDayAndTimeSlot(dayOfWeek, timeSlotId, academicYear);
            return timetables.ConvertAll(TimetableEntryMapper.ToDTO);
        }

        public List<TimetableEntryDto> GetTimetableEntriesByAcademicYear(string academicYear)
        {
            if (string.IsNullOrWhiteSpace(academicYear))
            {
                throw new ArgumentException("Academic year cannot be empty.", nameof(academicYear));
            }
            var timetables = _timetableRepository.GetTimetableEntriesByAcademicYear(academicYear);
            return timetables.ConvertAll(TimetableEntryMapper.ToDTO);
        }

        public List<TimetableEntryDto> GetAllTimetableEntries()
        {
            var timetables = _timetableRepository.GetAllTimetableEntries();
            return timetables.ConvertAll(TimetableEntryMapper.ToDTO);
        }
    }

    internal interface ITimetableService
    {
    }
}
