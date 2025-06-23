using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicom_Tic_Management_System.Models;
using Unicom_Tic_Management_System.Models.DTOs.Grouping_MentoringDTOs;
using Unicom_Tic_Management_System.Repositories;
using Unicom_Tic_Management_System.Repositories.Interfaces;
using Unicom_Tic_Management_System.Services.Interfaces;
using Unicom_Tic_Management_System.Utilities.Mappers;

namespace Unicom_Tic_Management_System.Services
{
    internal class MentorService : IMentorService
    {
        private readonly IMentorRepository _repository;

        public MentorService(IMentorRepository repository)
        {
            _repository = repository;
        }

        public MentorDto GetMentorById(int mentorId)
        {
            var mentor = _repository.GetMentorById(mentorId);
            return mentor != null ? MentorMapper.ToDTO(mentor) : null;
        }

        public MentorDto GetMentorByNic(string nic)
        {
            var mentor = _repository.GetMentorByNic(nic);
            return mentor != null ? MentorMapper.ToDTO(mentor) : null;
        }

        public List<MentorDto> GetAllMentors()
        {
            var mentors = _repository.GetAllMentors();
            return MentorMapper.ToDTOList(mentors);
        }

        public List<MentorDto> GetMentorsByDepartment(int departmentId)
        {
            var mentors = _repository.GetMentorsByDepartmentId(departmentId);
            return MentorMapper.ToDTOList(mentors);
        }

        public void AddMentor(MentorDto mentorDto)
        {
            if (mentorDto == null)
                throw new ArgumentNullException(nameof(mentorDto));

            var mentor = MentorMapper.ToEntity(mentorDto);
            _repository.AddMentor(mentor);
        }

        public void UpdateMentor(MentorDto mentorDto)
        {
            if (mentorDto == null)
                throw new ArgumentNullException(nameof(mentorDto));

            var mentor = MentorMapper.ToEntity(mentorDto);
            _repository.UpdateMentor(mentor);
        }

        public void DeleteMentor(int mentorId)
        {
            _repository.DeleteMentor(mentorId);
        }
    }
}
