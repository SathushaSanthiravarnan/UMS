using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Controllers
{
    internal class TimeSlotController
    {
        private readonly ITimeSlotService _service;

        public TimeSlotController(ITimeSlotService service)
        {
            _service = service;
        }

        public void AddTimeSlot(TimeSlot timeSlot)
        {
            _service.AddTimeSlot(timeSlot);
        }

        public void UpdateTimeSlot(TimeSlot timeSlot)
        {
            _service.UpdateTimeSlot(timeSlot);
        }

        public void DeleteTimeSlot(int timeSlotId)
        {
            _service.DeleteTimeSlot(timeSlotId);
        }

        public TimeSlot GetTimeSlotById(int timeSlotId)
        {
            return _service.GetTimeSlotById(timeSlotId);
        }

        public TimeSlot GetTimeSlotBySlotName(string slotName)
        {
            return _service.GetTimeSlotBySlotName(slotName);
        }

        public List<TimeSlot> GetAllTimeSlots()
        {
            return _service.GetAllTimeSlots();
        }
    }
}
