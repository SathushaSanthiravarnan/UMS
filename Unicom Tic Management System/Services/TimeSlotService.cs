using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Repositories.Interfaces;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Services
{
    internal class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotRepository _repository;

        public TimeSlotService(ITimeSlotRepository repository)
        {
            _repository = repository;
        }

        public void AddTimeSlot(TimeSlot timeSlot)
        {
            _repository.AddTimeSlot(timeSlot);
        }

        public void UpdateTimeSlot(TimeSlot timeSlot)
        {
            _repository.UpdateTimeSlot(timeSlot);
        }

        public void DeleteTimeSlot(int timeSlotId)
        {
            _repository.DeleteTimeSlot(timeSlotId);
        }

        public TimeSlot GetTimeSlotById(int timeSlotId)
        {
            return _repository.GetTimeSlotById(timeSlotId);
        }

        public TimeSlot GetTimeSlotBySlotName(string slotName)
        {
            return _repository.GetTimeSlotBySlotName(slotName);
        }

        public List<TimeSlot> GetAllTimeSlots()
        {
            return _repository.GetAllTimeSlots();
        }
    }
}
