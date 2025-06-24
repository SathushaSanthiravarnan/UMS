using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs;
using Unicom_Tic_Management_System.Repositories.Interfaces;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Services
{
    internal class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;

        public ExamService(IExamRepository examRepository)
        {
            _examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
        }

        public ExamDto GetExamById(int examId)
        {
            var exam = _examRepository.GetExamById(examId);
            if (exam == null) return null;
            return MapToDto(exam);
        }

        public List<ExamDto> GetAllExams()
        {
            var exams = _examRepository.GetAllExams();
            return exams.Select(MapToDto).ToList();
        }

        public List<ExamDto> GetExamsBySubject(int subjectId)
        {
            var exams = _examRepository.GetExamsBySubjectId(subjectId);
            return exams.Select(MapToDto).ToList();
        }

        public List<ExamDto> GetExamsByDate(DateTime examDate)
        {
            var exams = _examRepository.GetExamsByDate(examDate);
            return exams.Select(MapToDto).ToList();
        }

        public void AddExam(ExamDto examDto)
        {
            if (examDto == null) throw new ArgumentNullException(nameof(examDto));
            var exam = MapToModel(examDto);
            _examRepository.AddExam(exam);
        }

        public void UpdateExam(ExamDto examDto)
        {
            if (examDto == null) throw new ArgumentNullException(nameof(examDto));
            var exam = MapToModel(examDto);
            _examRepository.UpdateExam(exam);
        }

        public void DeleteExam(int examId)
        {
            _examRepository.DeleteExam(examId);
        }

        // Helper mapper methods between Exam and ExamDto
        private ExamDto MapToDto(Exam exam)
        {
            return new ExamDto
            {
                ExamId = exam.ExamId,
                ExamName = exam.ExamName,
                SubjectId = exam.SubjectId,
                ExamDate = exam.ExamDate,
                MaxMarks = exam.MaxMarks
            };
        }

        private Exam MapToModel(ExamDto dto)
        {
            return new Exam
            {
                ExamId = dto.ExamId,
                ExamName = dto.ExamName,
                SubjectId = dto.SubjectId,
                ExamDate = dto.ExamDate,
                MaxMarks = dto.MaxMarks
            };
        }
    }
}
