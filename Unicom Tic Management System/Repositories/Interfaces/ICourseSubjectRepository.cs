using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface ICourseSubjectRepository
    {
        void AddCourseSubject(CourseSubject courseSubject);
        CourseSubject GetCourseSubject(int courseId, int subjectId);
        List<CourseSubject> GetCourseSubjectsByCourseId(int courseId);
        List<CourseSubject> GetCourseSubjectsBySubjectId(int subjectId);
        void DeleteCourseSubject(int courseId, int subjectId);
    }
}
