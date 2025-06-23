using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models
{
    internal class Room
    {
        public int RoomId { get; set; } 
        public string RoomNumber { get; set; }
        public int? Capacity { get; set; }
        public string RoomType { get; set; }
    }
}
