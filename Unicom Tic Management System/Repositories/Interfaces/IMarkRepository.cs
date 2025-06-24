using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IMarkRepository
    {
        void AddMark(Mark mark);
        void UpdateMark(Mark mark);
        void DeleteMark(int markId);
        Mark GetMarkById(int markId);
        List<Mark> GetAllMarks();


        Mark GetMarkForStudentSubjectExam(int studentId, int subjectId, int examId);
        List<Mark> GetMarksByStudentId(int studentId);
        List<Mark> GetMarksBySubjectId(int subjectId);
        List<Mark> GetMarksByExamId(int examId);
        List<Mark> GetMarksByLecturerId(int lecturerId);


        List<MarkDisplayDto> GetAllMarksWithDetails();
        List<TopPerformerDto> GetTopNPerformers(int count);
    }
}
