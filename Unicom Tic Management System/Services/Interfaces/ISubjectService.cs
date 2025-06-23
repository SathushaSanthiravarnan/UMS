using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.AcademicDTOs;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface ISubjectService
    {
        SubjectDto GetSubjectById(int subjectId);
        SubjectDto GetSubjectByName(string subjectName);
        List<SubjectDto> GetAllSubjects();
        List<SubjectDto> GetSubjectsByCourse(int courseId);
        void AddSubject(SubjectDto subjectDto);
        void UpdateSubject(SubjectDto subjectDto);
        void DeleteSubject(int subjectId);
    }
}
