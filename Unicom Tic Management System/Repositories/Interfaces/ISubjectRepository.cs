using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface ISubjectRepository
    {
        void AddSubject(Subject subject);
        void UpdateSubject(Subject subject);
        void DeleteSubject(int subjectId);
        Subject GetSubjectById(int subjectId);
        Subject GetSubjectByNameAndCourse(string subjectName, int courseId); 
        List<Subject> GetSubjectsByCourseId(int courseId);
        List<Subject> GetAllSubjects();
    }
}
