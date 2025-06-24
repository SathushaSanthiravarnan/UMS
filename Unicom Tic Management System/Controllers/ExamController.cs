using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Controllers
{
    internal class ExamController
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService ?? throw new ArgumentNullException(nameof(examService));
        }

        public ExamDto GetExamById(int examId)
        {
            return _examService.GetExamById(examId);
        }

        public List<ExamDto> GetAllExams()
        {
            return _examService.GetAllExams();
        }

        public List<ExamDto> GetExamsBySubject(int subjectId)
        {
            return _examService.GetExamsBySubject(subjectId);
        }

        public List<ExamDto> GetExamsByDate(DateTime examDate)
        {
            return _examService.GetExamsByDate(examDate);
        }

        public void AddExam(ExamDto examDto)
        {
            _examService.AddExam(examDto);
        }

        public void UpdateExam(ExamDto examDto)
        {
            _examService.UpdateExam(examDto);
        }

        public void DeleteExam(int examId)
        {
            _examService.DeleteExam(examId);
        }
    }
}
