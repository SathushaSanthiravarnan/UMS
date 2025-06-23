using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.AcademicDTOs;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface IDepartmentService
    {
        DepartmentDto GetDepartmentById(int departmentId);
        DepartmentDto GetDepartmentByName(string departmentName);
        List<DepartmentDto> GetAllDepartments();
        void AddDepartment(DepartmentDto departmentDto);
        void UpdateDepartment(DepartmentDto departmentDto);
        void DeleteDepartment(int departmentId);
    }
}
