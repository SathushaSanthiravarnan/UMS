using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal class SubmissionMapper
    {
        public static SubmissionDto ToDTO(Submission submission)
        {
            if (submission == null) return null;
            return new SubmissionDto
            {
                SubmissionId = submission.SubmissionId,
                AssignmentId = submission.AssignmentId,
                StudentId = submission.StudentId,
                SubmittedAt = submission.SubmittedAt,
                Grade = submission.Grade,
                GradedByLecturerId = submission.GradedByLecturerId
            };
        }

        public static Submission ToEntity(SubmissionDto submissionDto)
        {
            if (submissionDto == null) return null;
            return new Submission
            {
                SubmissionId = submissionDto.SubmissionId,
                AssignmentId = submissionDto.AssignmentId,
                StudentId = submissionDto.StudentId,
                SubmittedAt = submissionDto.SubmittedAt,
                Grade = submissionDto.Grade,
                GradedByLecturerId = submissionDto.GradedByLecturerId
            };
        }

        public static List<SubmissionDto> ToDTOList(IEnumerable<Submission> submissions)
        {
            return submissions?.Select(ToDTO).ToList() ?? new List<SubmissionDto>();
        }

        public static List<Submission> ToEntityList(IEnumerable<SubmissionDto> submissionDtos)
        {
            return submissionDtos?.Select(ToEntity).ToList() ?? new List<Submission>();
        }
    }
}
