using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.Enums;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IStudentRepository
    {
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int studentId);
        Student GetStudentById(int studentId);
        Student GetStudentByNic(string nic);
        Student GetStudentByAdmissionNumber(string admissionNumber);
        Student GetStudentByEmail(string email);
        Student GetStudentByUserId(int userId);
        List<Student> GetStudentsByCourseId(int courseId);
        List<Student> GetStudentsByMainGroupId(int mainGroupId);
        List<Student> GetStudentsBySubGroupId(int subGroupId);
        List<Student> GetStudentsByGender(GenderType gender);
        List<Student> GetAllStudents();
    }
}
