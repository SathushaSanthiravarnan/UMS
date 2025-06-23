
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.AcademicOperationsDTOs;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface IMarkService
    {
        MarkDto GetMarkById(int markId);
        List<MarkDto> GetAllMarks();
        List<MarkDto> GetMarksByStudent(int studentId);
        List<MarkDto> GetMarksBySubject(int subjectId);
        List<MarkDto> GetMarksByExam(int examId);
        List<MarkDto> GetMarksGradedByLecturer(int lecturerId);
        void AddMark(MarkDto markDto);
        void UpdateMark(MarkDto markDto);
        void DeleteMark(int markId);
    }
}
