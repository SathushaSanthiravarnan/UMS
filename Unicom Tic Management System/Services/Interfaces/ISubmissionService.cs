using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface ISubmissionService
    {
        SubmissionDto GetSubmissionById(int submissionId);
        List<SubmissionDto> GetAllSubmissions();
        List<SubmissionDto> GetSubmissionsByAssignment(int assignmentId);
        List<SubmissionDto> GetSubmissionsByStudent(int studentId);
        List<SubmissionDto> GetSubmissionsGradedByLecturer(int lecturerId);
        void AddSubmission(SubmissionDto submissionDto);
        void UpdateSubmission(SubmissionDto submissionDto);
        void DeleteSubmission(int submissionId);
    }
}
