using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal class AssignmentMapper
    {
        public static AssignmentDto ToDTO(Assignment assignment)
        {
            if (assignment == null) return null;
            return new AssignmentDto
            {
                AssignmentId = assignment.AssignmentId,
                SubjectId = assignment.SubjectId,
                LecturerId = assignment.LecturerId,
                Title = assignment.Title,
                Description = assignment.Description,
                DueDate = assignment.DueDate,
                MaxMarks = assignment.MaxMarks
            };
        }

        public static Assignment ToEntity(AssignmentDto assignmentDto)
        {
            if (assignmentDto == null) return null;
            return new Assignment
            {
                AssignmentId = assignmentDto.AssignmentId,
                SubjectId = assignmentDto.SubjectId,
                LecturerId = assignmentDto.LecturerId,
                Title = assignmentDto.Title,
                Description = assignmentDto.Description,
                DueDate = assignmentDto.DueDate,
                MaxMarks = assignmentDto.MaxMarks
            };
        }

        public static List<AssignmentDto> ToDTOList(IEnumerable<Assignment> assignments)
        {
            return assignments?.Select(ToDTO).ToList() ?? new List<AssignmentDto>();
        }

        public static List<Assignment> ToEntityList(IEnumerable<AssignmentDto> assignmentDtos)
        {
            return assignmentDtos?.Select(ToEntity).ToList() ?? new List<Assignment>();
        }
    }
}
