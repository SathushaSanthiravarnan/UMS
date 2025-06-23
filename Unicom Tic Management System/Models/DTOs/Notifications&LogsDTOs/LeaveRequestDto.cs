using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models.DTOs.Notifications_LogsDTOs
{
    internal class LeaveRequestDto
    {
        public int LeaveId { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public Enums.LeaveStatus Status { get; set; }
        public int? ApprovedByUserId { get; set; }
    }
}
