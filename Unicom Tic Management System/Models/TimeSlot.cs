﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models
{
    internal class TimeSlot
    {
        public int TimeSlotId { get; set; }
        public string SlotName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
