using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.AcademicDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal static class SubjectMapper
    {
        public static SubjectDto ToDTO(Subject subject)
        {
            if (subject == null) return null;
            return new SubjectDto
            {
                SubjectId = subject.SubjectId,
                SubjectName = subject.SubjectName,
                CourseId = subject.CourseId,
                CourseName = subject.Course?.CourseName ?? ""


            };
        }

        public static Subject ToEntity(SubjectDto subjectDto)
        {
            if (subjectDto == null) return null;
            return new Subject
            {
                SubjectId = subjectDto.SubjectId,
                SubjectName = subjectDto.SubjectName,
                CourseId = subjectDto.CourseId,
              
            };
        }

        public static List<SubjectDto> ToDTOList(IEnumerable<Subject> subjects)
        {
            return subjects?.Select(s => ToDTO(s)).ToList() ?? new List<SubjectDto>();
        }

        public static List<Subject> ToEntityList(IEnumerable<SubjectDto> subjectDtos)
        {
            return subjectDtos?.Select(s => ToEntity(s)).ToList() ?? new List<Subject>();
        }
    }

}
