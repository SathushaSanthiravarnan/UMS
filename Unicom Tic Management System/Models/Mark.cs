using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models
{
    internal class Mark
    {
        public int MarkId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int ExamId { get; set; }
        public int MarksObtained { get; set; }
        public int? GradedByLecturerId { get; set; }
        public string Grade { get; set; }
        public DateTime EntryDate { get; set; }

    }
}
