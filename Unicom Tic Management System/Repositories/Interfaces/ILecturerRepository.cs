using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface ILecturerRepository
    {
        void AddLecturer(Lecturer lecturer);
        void UpdateLecturer(Lecturer lecturer);
        void DeleteLecturer(int lecturerId);
        Lecturer GetLecturerById(int lecturerId);
        Lecturer GetLecturerByNic(string nic);
        Lecturer GetLecturerByEmail(string email);
        Lecturer GetLecturerByUserId(int userId);
        List<Lecturer> GetLecturersByDepartmentId(int departmentId);
        List<Lecturer> GetAllLecturers();
    }
}
