using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface IExamService
    {
        ExamDto GetExamById(int examId);
        List<ExamDto> GetAllExams();
        List<ExamDto> GetExamsBySubject(int subjectId);
        List<ExamDto> GetExamsByDate(DateTime examDate);
        void AddExam(ExamDto examDto);
        void UpdateExam(ExamDto examDto);
        void DeleteExam(int examId);
    }
}
