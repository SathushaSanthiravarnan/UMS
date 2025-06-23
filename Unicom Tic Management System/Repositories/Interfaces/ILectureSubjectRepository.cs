using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface ILectureSubjectRepository
    {
        void AddLectureSubject(LectureSubject lectureSubject);
        void DeleteLectureSubject(int lecturerId, int subjectId);
        LectureSubject GetLectureSubject(int lecturerId, int subjectId);
        List<LectureSubject> GetSubjectsByLecturerId(int lecturerId);
        List<LectureSubject> GetLecturersBySubjectId(int subjectId);
        List<LectureSubject> GetAllLectureSubjects();
    }
}
