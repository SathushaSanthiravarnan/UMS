using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models
{
    internal class ActivityLog
    {
        public int LogId { get; set; }
        public int? UserId { get; set; } 
        public string Action { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
