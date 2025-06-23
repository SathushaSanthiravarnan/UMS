using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IExamRepository
    {
        void AddExam(Exam exam);
        void UpdateExam(Exam exam);
        void DeleteExam(int examId);
        Exam GetExamById(int examId);
        List<Exam> GetExamsBySubjectId(int subjectId);
        List<Exam> GetExamsByDate(DateTime examDate);
        List<Exam> GetAllExams();
    }
}
