using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IDepartmentRepository
    {
        void AddDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(int departmentId);
        Department GetDepartmentById(int departmentId);
        Department GetDepartmentByName(string departmentName);
        List<Department> GetAllDepartments();
    }
}
