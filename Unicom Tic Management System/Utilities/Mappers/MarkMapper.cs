using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal class MarkMapper
    {
        public static MarkDto ToDTO(Mark mark)
        {
            if (mark == null) return null;
            return new MarkDto
            {
                MarkId = mark.MarkId,
                StudentId = mark.StudentId,
                SubjectId = mark.SubjectId,
                ExamId = mark.ExamId,
                MarksObtained = mark.MarksObtained,
                Grade = mark.Grade,
                EntryDate = mark.EntryDate,
                GradedByLecturerId = mark.GradedByLecturerId
            };
        }

        public static Mark ToEntity(MarkDto markDto)
        {
            if (markDto == null) return null;
            return new Mark
            {
                MarkId = markDto.MarkId.Value,
                StudentId = markDto.StudentId,
                SubjectId = markDto.SubjectId,
                ExamId = markDto.ExamId,
                MarksObtained = markDto.MarksObtained,
                Grade = markDto.Grade,
                EntryDate = markDto.EntryDate,
                GradedByLecturerId = markDto.GradedByLecturerId.Value
            };
        }

        public static List<MarkDto> ToDTOList(IEnumerable<Mark> marks)
        {
            return marks?.Select(ToDTO).ToList() ?? new List<MarkDto>();
        }

        public static List<Mark> ToEntityList(IEnumerable<MarkDto> markDtos)
        {
            return markDtos?.Select(ToEntity).ToList() ?? new List<Mark>();
        }
    }
}
