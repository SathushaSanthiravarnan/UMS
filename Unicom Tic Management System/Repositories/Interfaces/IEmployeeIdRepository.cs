using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IEmployeeIdRepository
    {
        void AddEmployeeId(EmployeeId employeeId);
        void UpdateEmployeeId(EmployeeId employeeId); 
        void DeleteEmployeeId(string employeeIdText);
        EmployeeId GetEmployeeIdByText(string employeeIdText);
        EmployeeId GetEmployeeIdByUserId(int userId);
        List<EmployeeId> GetAllEmployeeIds();
    }
}
