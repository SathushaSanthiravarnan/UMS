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
    internal class TimetableEntryService : ITimetableEntryService
    {
        private readonly ITimetableRepository _repository;

        public TimetableEntryService(ITimetableRepository repository)
        {
            _repository = repository;
        }

        public void AddTimetableEntry(TimetableEntryDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var entity = TimetableEntryMapper.ToEntity(dto);
            _repository.AddTimetableEntry(entity);
        }

        public void UpdateTimetableEntry(TimetableEntryDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var entity = TimetableEntryMapper.ToEntity(dto);
            _repository.UpdateTimetableEntry(entity);
        }

        public void DeleteTimetableEntry(int timetableId)
        {
            if (timetableId <= 0) throw new ArgumentException("Invalid timetable ID");
            _repository.DeleteTimetableEntry(timetableId);
        }

        public TimetableEntryDto GetTimetableEntryById(int timetableId)
        {
            var entity = _repository.GetTimetableEntryById(timetableId);
            return TimetableEntryMapper.ToDTO(entity);
        }

        public List<TimetableEntryDto> GetAllTimetableEntries()
        {
            var entities = _repository.GetAllTimetableEntries();
            return TimetableEntryMapper.ToDTOList(entities);
        }

        public List<TimetableEntryDto> GetTimetableForCourse(int courseId, string academicYear)
        {
            var entities = _repository.GetTimetableEntriesByCourse(courseId, academicYear);
            return TimetableEntryMapper.ToDTOList(entities);
        }

        public List<TimetableEntryDto> GetTimetableForLecturer(int lecturerId, string academicYear)
        {
            var entities = _repository.GetTimetableEntriesByLecturer(lecturerId, academicYear);
            return TimetableEntryMapper.ToDTOList(entities);
        }

        public List<TimetableEntryDto> GetTimetableForRoom(int roomId, string academicYear)
        {
            var entities = _repository.GetTimetableEntriesByRoom(roomId, academicYear);
            return TimetableEntryMapper.ToDTOList(entities);
        }

        public List<TimetableEntryDto> GetTimetableForDay(string dayOfWeek, string academicYear)
        {
            var entities = _repository.GetTimetableEntriesByDayAndTimeSlot(dayOfWeek, -1, academicYear);
            return TimetableEntryMapper.ToDTOList(entities);
        }
    }
}
