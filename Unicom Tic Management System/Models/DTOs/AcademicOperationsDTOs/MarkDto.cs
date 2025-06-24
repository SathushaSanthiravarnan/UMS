using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs
{
    internal class MarkDto
    {
        public int? MarkId { get; set; }

      
        public int StudentId { get; set; }

       
        public int SubjectId { get; set; }

        
        public int ExamId { get; set; }

        public int MarksObtained { get; set; }

        public int? GradedByLecturerId { get; set; }

        public string Grade { get; set; }

        
        public DateTime EntryDate { get; set; }
    }

    public class MarkDisplayDto
    {
        public int MarkId { get; set; }
        public string StudentAdmissionNumber { get; set; }
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public string ExamType { get; set; }
        public int MarksObtained { get; set; }
        public string GradedByLecturerName { get; set; }
        public string Grade { get; set; }
        public DateTime EntryDate { get; set; }

        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int ExamId { get; set; }
        public int? GradedByLecturerId { get; set; }
    }

    public class TopPerformerDto
    {
        public string AdmissionNumber { get; set; }
        public string StudentName { get; set; }
        public double AverageMarks { get; set; }
    }
}
