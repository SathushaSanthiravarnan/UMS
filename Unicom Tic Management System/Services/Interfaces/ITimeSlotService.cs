using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface ITimeSlotService
    {
        void AddTimeSlot(TimeSlot timeSlot);
        void UpdateTimeSlot(TimeSlot timeSlot);
        void DeleteTimeSlot(int timeSlotId);
        TimeSlot GetTimeSlotById(int timeSlotId);
        TimeSlot GetTimeSlotBySlotName(string slotName);
        List<TimeSlot> GetAllTimeSlots();
    }
}
