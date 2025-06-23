using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.AcademicDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal static class DepartmentMapper
    {
        public static DepartmentDto ToDTO(Department department)
        {
            if (department == null) return null;
            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            };
        }

        public static Department ToEntity(DepartmentDto departmentDto)
        {
            if (departmentDto == null) return null;
            return new Department
            {
                DepartmentId = departmentDto.DepartmentId,
                DepartmentName = departmentDto.DepartmentName
            };
        }

        public static List<DepartmentDto> ToDTOList(IEnumerable<Department> departments)
        {
            return departments?.Select(d => ToDTO(d)).ToList() ?? new List<DepartmentDto>();
        }

        public static List<Department> ToEntityList(IEnumerable<DepartmentDto> departmentDtos)
        {
            return departmentDtos?.Select(d => ToEntity(d)).ToList() ?? new List<Department>();
        }
    }
}
