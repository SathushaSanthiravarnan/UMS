using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models.DTOs.Scheduling_AttendanceDTOs
{
    internal class RoomDto
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public int? Capacity { get; set; }
        public string RoomType { get; set; }
    }
}
