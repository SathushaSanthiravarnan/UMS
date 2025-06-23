using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal class ExamMapper
    {
        public static ExamDto ToDTO(Exam exam)
        {
            if (exam == null) return null;
            return new ExamDto
            {
                ExamId = exam.ExamId,
                ExamName = exam.ExamName,
                SubjectId = exam.SubjectId,
                ExamDate = exam.ExamDate,
                MaxMarks = exam.MaxMarks
            };
        }

        public static Exam ToEntity(ExamDto examDto)
        {
            if (examDto == null) return null;
            return new Exam
            {
                ExamId = examDto.ExamId,
                ExamName = examDto.ExamName,
                SubjectId = examDto.SubjectId,
                ExamDate = examDto.ExamDate,               
                MaxMarks = examDto.MaxMarks
            };
        }

        public static List<ExamDto> ToDTOList(IEnumerable<Exam> exams)
        {
            return exams?.Select(ToDTO).ToList() ?? new List<ExamDto>();
        }

        public static List<Exam> ToEntityList(IEnumerable<ExamDto> examDtos)
        {
            return examDtos?.Select(ToEntity).ToList() ?? new List<Exam>();
        }
    }
}
