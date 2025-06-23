using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models
{
    internal class Notification
    {
        public int NotificationId { get; set; } 
        public string Title { get; set; }
        public string Message { get; set; }
        public int? SentToUserId { get; set; } 
        public string SentToRole { get; set; } 
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }
}
