using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.Grouping_MentoringDTOs;

namespace Unicom_Tic_Management_System.Utilities.Mappers
{
    internal static class MentorMapper
    {
        public static MentorDto ToDTO(Mentor mentor)
        {
            if (mentor == null) return null;
            return new MentorDto
            {
                MentorId = mentor.MentorId,
                Name = mentor.Name,
                Nic = mentor.Nic,
                Email = mentor.Email,
                Phone = mentor.Phone,
                DepartmentId = mentor.DepartmentId
            };
        }

        public static Mentor ToEntity(MentorDto mentorDto)
        {
            if (mentorDto == null) return null;
            return new Mentor
            {
                MentorId = mentorDto.MentorId,
                Name = mentorDto.Name,
                Nic = mentorDto.Nic,
                Email = mentorDto.Email,
                Phone = mentorDto.Phone,
                DepartmentId = mentorDto.DepartmentId
            };
        }

        public static List<MentorDto> ToDTOList(IEnumerable<Mentor> mentors)
        {
            return mentors?.Select(m => ToDTO(m)).ToList() ?? new List<MentorDto>();
        }

        public static List<Mentor> ToEntityList(IEnumerable<MentorDto> mentorDtos)
        {
            return mentorDtos?.Select(m => ToEntity(m)).ToList() ?? new List<Mentor>();
        }
    }

}
