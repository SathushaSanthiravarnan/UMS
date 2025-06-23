using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models
{
    internal class BackupLog
    {
        public int BackupLogId { get; set; } 
        public DateTime CreatedAt { get; set; }
        public string BackupPath { get; set; }
        public string Status { get; set; } 
        public int? PerformedByUserId { get; set; }
    }
}
