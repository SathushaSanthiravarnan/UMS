using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models.DTOs.Notifications_LogsDTOs
{
    internal class StudentComplaintDto
    {
        public int ComplaintId { get; set; }
        public int StudentId { get; set; }
        public string ComplaintText { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public int? ResolvedByUserId { get; set; }
        public string ResolutionNotes { get; set; }
    }
}
