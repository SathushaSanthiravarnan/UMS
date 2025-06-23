using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IAssignmentRepository
    {
        void AddAssignment(Assignment assignment);
        void UpdateAssignment(Assignment assignment);
        void DeleteAssignment(int assignmentId);
        Assignment GetAssignmentById(int assignmentId);
        List<Assignment> GetAssignmentsBySubjectId(int subjectId);
        List<Assignment> GetAssignmentsByLecturerId(int lecturerId);
        List<Assignment> GetAllAssignments();
    }
}
