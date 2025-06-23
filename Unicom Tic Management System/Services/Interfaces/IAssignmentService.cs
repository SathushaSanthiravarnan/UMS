using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface IAssignmentService
    {
        AssignmentDto GetAssignmentById(int assignmentId);
        List<AssignmentDto> GetAllAssignments();
        List<AssignmentDto> GetAssignmentsBySubject(int subjectId);
        List<AssignmentDto> GetAssignmentsByLecturer(int lecturerId);
        List<AssignmentDto> GetUpcomingAssignments(DateTime untilDate);
        void AddAssignment(AssignmentDto assignmentDto);
        void UpdateAssignment(AssignmentDto assignmentDto);
        void DeleteAssignment(int assignmentId);
    }
}
