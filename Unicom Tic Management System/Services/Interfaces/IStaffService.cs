using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.StaffDTOs;

namespace Unicom_Tic_Management_System.Services.Interfaces
{
    internal interface IStaffService
    {
        StaffDto GetStaffById(int staffId);
        StaffDto GetStaffByNic(string nic);
        List<StaffDto> GetAllStaff();
        List<StaffDto> GetStaffByDepartment(int departmentId);
        void AddStaff(StaffDto staffDto);
        void UpdateStaff(StaffDto staffDto);
        void DeleteStaff(int staffId);
    }
}
