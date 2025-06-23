using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Controllers
{
    internal class TimetableController
    {
        private readonly ITimetableEntryService _service;

        public TimetableController(ITimetableEntryService service)
        {
            _service = service;
        }

        public void AddTimetableEntry(TimetableEntryDto entry) => _service.AddTimetableEntry(entry);
        public void UpdateTimetableEntry(TimetableEntryDto entry) => _service.UpdateTimetableEntry(entry);
        public void DeleteTimetableEntry(int id) => _service.DeleteTimetableEntry(id);
        public List<TimetableEntryDto> GetAll() => _service.GetAllTimetableEntries();
    }
}

