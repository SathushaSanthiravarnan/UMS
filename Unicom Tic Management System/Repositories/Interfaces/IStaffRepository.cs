using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;

namespace Unicom_Tic_Management_System.Repositories.Interfaces
{
    internal interface IStaffRepository
    {
        void AddStaff(Staff staff);
        void UpdateStaff(Staff staff);
        void DeleteStaff(int staffId);
        Staff GetStaffById(int staffId);
        Staff GetStaffByNic(string nic);
        Staff GetStaffByEmail(string email);
        Staff GetStaffByUserId(int userId);
        List<Staff> GetStaffByDepartmentId(int departmentId);
        List<Staff> GetAllStaff();
    }
}
