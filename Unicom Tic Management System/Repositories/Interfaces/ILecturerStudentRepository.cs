using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface ILecturerStudentRepository
    {
        void AddLecturerStudent(LecturerStudent lecturerStudent);
        void UpdateLecturerStudent(LecturerStudent lecturerStudent); 
        void DeleteLecturerStudent(int lecturerId, int studentId);
        LecturerStudent GetLecturerStudent(int lecturerId, int studentId);
        List<LecturerStudent> GetStudentsByLecturerId(int lecturerId);
        List<LecturerStudent> GetLecturersByStudentId(int studentId);
        List<LecturerStudent> GetAllLecturerStudentRelationships();
    }
}
