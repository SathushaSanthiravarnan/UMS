using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface ISubmissionRepository
    {
        void AddSubmission(Submission submission);
        void UpdateSubmission(Submission submission); 
        void DeleteSubmission(int submissionId);
        Submission GetSubmissionById(int submissionId);
        Submission GetSubmissionByAssignmentAndStudent(int assignmentId, int studentId); 
        List<Submission> GetSubmissionsByAssignmentId(int assignmentId);
        List<Submission> GetSubmissionsByStudentId(int studentId);
        List<Submission> GetSubmissionsByLecturerId(int lecturerId); 
        List<Submission> GetAllSubmissions();
    }
}
