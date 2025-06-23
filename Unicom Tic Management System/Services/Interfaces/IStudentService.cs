using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.Grouping_MentoringDTOs;
using Unicom_Tic_Management_System.Models.Enums;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface IStudentService
    {
        StudentDto GetStudentById(int studentId);
        StudentDto GetStudentByNic(string nic);
        StudentDto GetStudentByAdmissionNumber(string admissionNumber);
        StudentDto GetStudentByEmail(string email);
        StudentDto GetStudentByUserId(int userId);
        List<StudentDto> GetAllStudents();
        List<StudentDto> GetStudentsByCourseId(int courseId);
        List<StudentDto> GetStudentsByMainGroupId(int mainGroupId);
        List<StudentDto> GetStudentsBySubGroupId(int subGroupId);
        List<StudentDto> GetStudentsByGender(GenderType gender);
        void AddStudent(StudentDto studentDto);
        void UpdateStudent(StudentDto studentDto);
        void DeleteStudent(int studentId);
    }
}
