using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models
{
    internal class Submission
    {
        public int SubmissionId { get; set; } 
        public int AssignmentId { get; set; } 
        public int StudentId { get; set; } 
        public DateTime SubmittedAt { get; set; }
        public int? Grade { get; set; }
        public int? GradedByLecturerId { get; set; }
    }
}
