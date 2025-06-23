using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.StaffDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal static class StaffMapper
    {
        public static StaffDto ToDTO(Staff staff)
        {
            if (staff == null) return null;
            return new StaffDto
            {
                StaffId = staff.StaffId,
                Name = staff.Name,
                Nic = staff.Nic,
                DepartmentId = staff.DepartmentId,
                
                ContactNo = staff.ContactNo,
                Email = staff.Email,
                HireDate = staff.HireDate
            };
        }

        public static Staff ToEntity(StaffDto staffDto)
        {
            if (staffDto == null) return null;
            return new Staff
            {
                StaffId = staffDto.StaffId,
                Name = staffDto.Name,
                Nic = staffDto.Nic,
                DepartmentId = staffDto.DepartmentId,
                
                ContactNo = staffDto.ContactNo,
                Email = staffDto.Email,
                HireDate = staffDto.HireDate
            };
        }

        public static List<StaffDto> ToDTOList(IEnumerable<Staff> staffMembers)
        {
            return staffMembers?.Select(s => ToDTO(s)).ToList() ?? new List<StaffDto>();
        }

        public static List<Staff> ToEntityList(IEnumerable<StaffDto> staffDtos)
        {
            return staffDtos?.Select(s => ToEntity(s)).ToList() ?? new List<Staff>();
        }
    }
}
