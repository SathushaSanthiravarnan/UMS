using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs
{
    internal class ExamDto
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public int SubjectId { get; set; }
        public DateTime ExamDate { get; set; }
        public int MaxMarks { get; set; }
    }
}
