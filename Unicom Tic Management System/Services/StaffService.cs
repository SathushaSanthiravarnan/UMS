using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models.DTOs.StaffDTOs;
using Unicom_Tic_Management_System.Repositories.Interfaces;
using Unicom_Tic_Management_System.Services.Interfaces;
using Unicom_Tic_Management_System.Utilities.Mappers;

namespace Unicom_Tic_Management_System.Services
{
    internal class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public void AddStaff(StaffDto staffDto)
        {
            var staff = StaffMapper.ToEntity(staffDto);
            staff.CreatedAt = DateTime.Now;
            staff.UpdatedAt = DateTime.Now;
            _staffRepository.AddStaff(staff);
        }

        public void UpdateStaff(StaffDto staffDto)
        {
            var staff = StaffMapper.ToEntity(staffDto);
            staff.UpdatedAt = DateTime.Now;
            _staffRepository.UpdateStaff(staff);
        }

        public void DeleteStaff(int staffId)
        {
            _staffRepository.DeleteStaff(staffId);
        }

        public StaffDto GetStaffById(int staffId)
        {
            var staff = _staffRepository.GetStaffById(staffId);
            return staff != null ? StaffMapper.ToDTO(staff) : null;
        }

        public StaffDto GetStaffByNic(string nic)
        {
            var staff = _staffRepository.GetStaffByNic(nic);
            return staff != null ? StaffMapper.ToDTO(staff) : null;
        }

        public List<StaffDto> GetAllStaff()
        {
            var staffList = _staffRepository.GetAllStaff();
            return staffList.ConvertAll(StaffMapper.ToDTO);
        }

        public List<StaffDto> GetStaffByDepartment(int departmentId)
        {
            var staffList = _staffRepository.GetStaffByDepartmentId(departmentId);
            return staffList.ConvertAll(StaffMapper.ToDTO);
        }
    }
}
