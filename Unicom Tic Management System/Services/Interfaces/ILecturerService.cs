using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.StaffDTOs;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface ILecturerService
    {
        LecturerDto GetLecturerById(int lecturerId);
        LecturerDto GetLecturerByNic(string nic);
        List<LecturerDto> GetAllLecturers();
        List<LecturerDto> GetLecturersByDepartment(int departmentId);
        void AddLecturer(LecturerDto lecturerDto);
        void UpdateLecturer(LecturerDto lecturerDto);
        void DeleteLecturer(int lecturerId);
    }
}
