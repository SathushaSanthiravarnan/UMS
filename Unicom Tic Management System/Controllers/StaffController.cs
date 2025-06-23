using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.StaffDTOs;
using Unicom_Tic_Management_System.Services.Interfaces;

namespace Unicom_Tic_Management_System.Controllers
{
    internal class StaffController
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        public void AddStaff(StaffDto staffDto)
        {
            _staffService.AddStaff(staffDto);
        }

        public void UpdateStaff(StaffDto staffDto)
        {
            _staffService.UpdateStaff(staffDto);
        }

        public void DeleteStaff(int staffId)
        {
            _staffService.DeleteStaff(staffId);
        }

        public StaffDto GetStaffById(int staffId)
        {
            return _staffService.GetStaffById(staffId);
        }

        public StaffDto GetStaffByNic(string nic)
        {
            return _staffService.GetStaffByNic(nic);
        }

        public List<StaffDto> GetAllStaff()
        {
            return _staffService.GetAllStaff();
        }

        public List<StaffDto> GetStaffByDepartment(int departmentId)
        {
            return _staffService.GetStaffByDepartment(departmentId);
        }
    }
}

