
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
        void AddMark(MarkDto markDto); 
        void UpdateMark(MarkDto markDto); 
        void DeleteMark(int markId);

        
        MarkDisplayDto GetMarkDisplayById(int markId); 


        List<MarkDisplayDto> GetAllMarkDetails();

        
        List<MarkDisplayDto> GetMarkDetailsByStudentId(int studentId);
        List<MarkDisplayDto> GetMarkDetailsBySubjectId(int subjectId);
        List<MarkDisplayDto> GetMarkDetailsByExamId(int examId);
        List<MarkDisplayDto> GetMarkDetailsByLecturerId(int lecturerId);


        List<TopPerformerDto> GetTopPerformers(int count);
        string CalculateGrade(int marksObtained); 
    }
}

