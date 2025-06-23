using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.StaffDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal static class LecturerMapper
    {
        public static LecturerDto ToDTO(Lecturer lecturer)
        {
            if (lecturer == null) return null;
            return new LecturerDto
            {
                LecturerId = lecturer.LecturerId,
                Name = lecturer.Name,
                Nic = lecturer.Nic,
                Phone = lecturer.Phone,
                Email = lecturer.Email,
                DepartmentId = lecturer.DepartmentId,
                HireDate = lecturer.HireDate
            };
        }

        public static Lecturer ToEntity(LecturerDto lecturerDto)
        {
            if (lecturerDto == null) return null;
            return new Lecturer
            {
                LecturerId = lecturerDto.LecturerId,
                Name = lecturerDto.Name,
                Nic = lecturerDto.Nic,
                Phone = lecturerDto.Phone,
                Email = lecturerDto.Email,
                DepartmentId = lecturerDto.DepartmentId,
                HireDate = lecturerDto.HireDate
            };
        }

        public static List<LecturerDto> ToDTOList(IEnumerable<Lecturer> lecturers)
        {
            return lecturers?.Select(l => ToDTO(l)).ToList() ?? new List<LecturerDto>();
        }

        public static List<Lecturer> ToEntityList(IEnumerable<LecturerDto> lecturerDtos)
        {
            return lecturerDtos?.Select(l => ToEntity(l)).ToList() ?? new List<Lecturer>();
        }
    }
}
